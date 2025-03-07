namespace Connectify.Models
{
    public class RefreshToken
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokeAt { get; set; }
        public bool IsActive => RevokeAt == null && !IsExpired;

        public Guid UserId {  get; set; }
        public User user { get; set; } = null!;
    }
}
