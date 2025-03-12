using Connectify.Models;

public interface IUserService
{
    public Task<UserDto?> GetUserByIdAsync(Guid userId);
    public Task<UserDto?> GetUserByUsernameAsync(string username);
    public Task<string> UpdateUserAvatarAsync(Guid userId, string avatarUrl);
}