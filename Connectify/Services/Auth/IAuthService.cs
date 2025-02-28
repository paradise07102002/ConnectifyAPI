public interface IAuthService
{
    Task<bool> RegisterUserAsync(RegisterDto dto);
    Task<(LoginResult, string?)> LoginAsync(LoginDto dto);
    Task<string?> ConfirmEmailAsync(string token);
}