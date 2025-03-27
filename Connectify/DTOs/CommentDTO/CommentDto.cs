using Connectify.Models;

public class CommentDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public Guid PostId { get; set; }

    public Guid UserId { get; set; }
    public String? avatarUrl { get; set; }
    public String? fullName { get; set; }

}