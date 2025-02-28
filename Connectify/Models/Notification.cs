using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class Notification
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SenderId { get; set; }

    public string? Type { get; set; }

    public int ReferenceId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsRead { get; set; }

    public virtual User Sender { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
