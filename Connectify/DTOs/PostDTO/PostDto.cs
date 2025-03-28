using Connectify.Models;

public class PostDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public PrivacyLevel PrivacyLevel { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }

    public Guid userId { get; set; }
    public string? AvatarUrl { get; set; }
    public string? FullName { get; set; }
}