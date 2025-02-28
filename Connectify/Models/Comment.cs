using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Comment1 { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
