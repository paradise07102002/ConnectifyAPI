namespace Connectify.Models
{
    public class Share
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Caption { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        
        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;

    }
}
