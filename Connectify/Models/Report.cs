using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class Report
{
    public int Id { get; set; }

    public int ReporterId { get; set; }

    public int? ReportedUserId { get; set; }

    public int? ReportedPostId { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual Post? ReportedPost { get; set; }

    public virtual User? ReportedUser { get; set; }

    public virtual User Reporter { get; set; } = null!;
}
