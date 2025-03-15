namespace Connectify.Models
{
    public class PostMedia
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string MediaUrl { get; set; } = null!;
        public MediaType Type { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}
