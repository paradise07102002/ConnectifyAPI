using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Bio { get; set; }

    public string? AvatarUrl { get; set; }

    public string? CoverUrl { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? VerificationToken { get; set; }

    public bool IsVerified { get; set; }

    public bool IsActive { get; set; }
    
    public String? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiry {  get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = new();
}
