using Connectify.Models;

public interface IUserService
{
    public Task<UserDto?> GetUserByIdAsync(Guid userId);
    public Task<UserDto?> GetUserByUsernameAsync(string username);
}