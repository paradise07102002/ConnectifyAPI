using Connectify.Models;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid userId);
    Task<User?> GetByUsernameAsync(string username);
    Task UpdateUserAvatarAsync(Guid userId, string avatarUrl);
}