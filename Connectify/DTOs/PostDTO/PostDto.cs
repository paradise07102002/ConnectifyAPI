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
    public List<PostMedia> Medias { get; set; } = null!;
    public List<CommentDto> Comments { get; set; } = null!;
    public List<Like> Likes { get; set; } = null!;
    public List<Share> shares { get; set; } = null!;
}