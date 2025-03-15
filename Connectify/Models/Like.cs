namespace Connectify.Models
{
    public class Like
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;

        public Guid userId { get; set; }
        public User User { get; set; } = null!;

    }
}
