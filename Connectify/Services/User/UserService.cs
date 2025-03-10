using Connectify.Models;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        return user == null ? null : MapToDto(user);
    }

    public async Task<UserDto?> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        return user == null ? null : MapToDto(user);
    }

    private UserDto MapToDto(Connectify.Models.User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            FullName = user.FullName,
            Bio = user.Bio,
            AvatarUrl = user.AvatarUrl,
            CreatedAt = user.CreatedAt,
        };
    }
}