using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Connectify.Models;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;
    public AuthService(IAuthRepository authRepository, IEmailSender emailSender, IConfiguration configuration)
    {
        _authRepository = authRepository;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public async Task<bool> RegisterUserAsync(RegisterDto dto)
    {
        if (await _authRepository.EmailExistsAsync(dto.Email)) return false;

        var newUser = new User
        {
            FullName = dto.FullName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = PasswordHasher.HashPassword(dto.Password),
            VerificationToken = GenerateVerificationToken(dto.Email),
            IsVerified = false,
            CreatedAt = DateTime.UtcNow,
        };

        await _authRepository.AddUserAsync(newUser);
        await _authRepository.SaveChangesAsync();

        string apiUrl = _configuration["AppSettings:ApiUrl"];
        string confirmationLink = $"{apiUrl}/api/auth/confirm-email?token={newUser.VerificationToken}";

        await _emailSender.SendEmailAsync(dto.Email, "Xác nhận email", $"Nhấn vào link sau để xác thực tài khoản: <a href='{confirmationLink}'>Xác nhận</>");

        return true;
    }

    public async Task<string?> ConfirmEmailAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Issuer"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null) return "Token không hợp lệ hoặc đã hết hạn";

            var user = await _authRepository.GetUserByEmailAsync(email);
            if (user == null || user.IsVerified) return "Token không hợp lệ hoặc đã hết hạn";

            user.IsVerified = true;
            user.VerificationToken = null;
            await _authRepository.SaveChangesAsync();
            return null;
        }
        catch (Exception)
        {
            return "Token không hợp lệ hoặc đã hết hạn!";
        }
    }

    public async Task<(LoginResult, string?, string?)> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetUserByEmailAsync(dto.Email);
        if (user == null || !PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return (LoginResult.InvalidCredentials, null, null);

        if (!user.IsVerified)
            return (LoginResult.EmailNotVerified, null, null);

        var accessToken = GenerateJwtToken(user);
        var refreshToken = GenerateRefreshToken();

        var newRefreshToken = new RefreshToken
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
        };

        //user.RefreshToken = refreshToken;
        //user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _authRepository.AddRefreshTokenAsync(newRefreshToken);
        await _authRepository.SaveChangesAsync();

        return (LoginResult.Success, accessToken, refreshToken);
    }

    public async Task<(bool IsSuccess, string? AccessToken, string? RefreshToken, string? ErrorMessage)> RefreshAccessTokenAsync(string refreshToken)
    {
        var user = await _authRepository.GetUserByRefreshTokenAsync(refreshToken);
        if (user == null || user.RefreshTokenExpiry <= DateTime.UtcNow)
        {
            return (false, null, null, "Refresh Token không hợp lệ hoặc đã hết hạn");
        }

        var newAccessToken = GenerateJwtToken(user);

        var newRefreshToken = GenerateRefreshToken();
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await _authRepository.SaveChangesAsync();

        return (true, newAccessToken, newRefreshToken, null);
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
    };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.UtcNow.AddHours(5),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateVerificationToken(string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(ClaimTypes.Email, email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
    };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Issuer"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}