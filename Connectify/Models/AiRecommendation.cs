using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class AiRecommendation
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? RecommendedUserId { get; set; }

    public int? RecommendedPostId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Post? RecommendedPost { get; set; }

    public virtual User? RecommendedUser { get; set; }

    public virtual User User { get; set; } = null!;
}
