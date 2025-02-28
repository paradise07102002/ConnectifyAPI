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

}
