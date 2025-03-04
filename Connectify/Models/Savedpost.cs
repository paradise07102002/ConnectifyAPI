﻿using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class Savedpost
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
