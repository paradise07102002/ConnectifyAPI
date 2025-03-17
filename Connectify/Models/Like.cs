using System.Text.Json.Serialization;

namespace Connectify.Models
{
    public class Like
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; } = null!;

        public Guid userId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;

    }
}
