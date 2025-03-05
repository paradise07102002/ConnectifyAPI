public interface IAuthService
{
    Task<bool> RegisterUserAsync(RegisterDto dto);
    Task<(LoginResult, string?, string?)> LoginAsync(LoginDto dto);
    Task<string?> ConfirmEmailAsync(string token);
    Task<(bool IsSuccess, string? AccessToken, string? RefreshToken, string? ErrorMessage)> RefreshAccessTokenAsync(string refreshToken);
}