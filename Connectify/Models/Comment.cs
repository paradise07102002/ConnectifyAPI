using System.Text.Json.Serialization;

namespace Connectify.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; } = null!;

        public Guid UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
    }
}
