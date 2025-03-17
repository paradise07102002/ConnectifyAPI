using System.Text.Json.Serialization;

namespace Connectify.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Content { get; set; }
        public PrivacyLevel PrivacyLevel { get; set; } = PrivacyLevel.Public;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateAt { get; set; }

        public Guid userId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;

        public List<PostMedia> Medias { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<Like> Likes { get; set; } = new();
        public List<Share> shares { get; set; } = new();

    }
}
