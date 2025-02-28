using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AiRecommendation> AiRecommendations { get; set; } = new List<AiRecommendation>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Savedpost> Savedposts { get; set; } = new List<Savedpost>();

    public virtual User User { get; set; } = null!;
}
