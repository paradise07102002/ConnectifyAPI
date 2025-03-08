using Connectify.DTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // Constructor to inject the authentication service
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Endpoint for user registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var isRegistered = await _authService.RegisterUserAsync(request);
        if (!isRegistered)
        {
            return BadRequest(new { message = "Email already exists" });
        }

        return StatusCode(201, new { message = "Registered successfully! Please verify your email." });
    }

    // Endpoint to confirm user email using a token
    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        var errorMessage = await _authService.ConfirmEmailAsync(token);
        if (errorMessage != null)
        {
            return BadRequest(new { message = errorMessage });
        }

        return Ok(new { message = "Email verification successful" });
    }

    // Endpoint for user login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var (result, accessToken, refreshToken) = await _authService.LoginAsync(dto);

        switch (result)
        {
            case LoginResult.InvalidCredentials:
                return Unauthorized(new { message = "Invalid email or password!" });

            case LoginResult.EmailNotVerified:
                return StatusCode(403, new { message = "Email not verified. Please check your email for verification." });

            case LoginResult.Success:
                return Ok(new { accessToken, refreshToken });

            default:
                return StatusCode(500, new { message = "Unknown error." });
        }
    }

    // Endpoint to refresh access token
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshAccessTokenAsync(request.RefreshToken);

        if (!result.IsSuccess)
            return BadRequest(new { message = result.ErrorMessage });

        return Ok(new
        {
            accessToken = result.AccessToken,
            refreshToken = result.RefreshToken
        });
    }

    // Endpoint for user logout
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
    {
        var success = await _authService.LogoutAsync(dto.RefreshToken);

        if (!success)
        {
            return BadRequest(new { message = "Logout failed. Invalid refresh token!" });
        }

        return Ok(new { message = "Logout successful!" });
    }
}