public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }
    public string? CoverUrl { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
}