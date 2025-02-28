using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? VerificationToken { get; set; }

    public bool IsVerified { get; set; }

    public string? Avatar { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AiRecommendation> AiRecommendationRecommendedUsers { get; set; } = new List<AiRecommendation>();

    public virtual ICollection<AiRecommendation> AiRecommendationUsers { get; set; } = new List<AiRecommendation>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Friendship> FriendshipFriends { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipUsers { get; set; } = new List<Friendship>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Notification> NotificationSenders { get; set; } = new List<Notification>();

    public virtual ICollection<Notification> NotificationUsers { get; set; } = new List<Notification>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Report> ReportReportedUsers { get; set; } = new List<Report>();

    public virtual ICollection<Report> ReportReporters { get; set; } = new List<Report>();

    public virtual ICollection<Savedpost> Savedposts { get; set; } = new List<Savedpost>();
}
