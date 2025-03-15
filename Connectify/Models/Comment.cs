namespace Connectify.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Content { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
