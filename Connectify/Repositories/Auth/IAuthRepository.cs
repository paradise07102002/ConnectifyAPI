using Connectify.Models;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email);
    Task AddUserAsync(User user);
    Task<User?> GetUserByEmailAsync(string email);
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task SaveChangesAsync();
}