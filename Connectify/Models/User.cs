using System;
using System.Collections.Generic;

namespace Connectify.Models;

public partial class User
{
    public Guid Id { get; set; }  // UUID thay vì int

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? FullName { get; set; } // Họ và tên

    public string? Bio { get; set; } // Tiểu sử cá nhân

    public string? AvatarUrl { get; set; } // Ảnh đại diện

    public string? CoverUrl { get; set; } // Ảnh bìa

    public DateTime? DateOfBirth { get; set; } // Ngày sinh

    public string? Gender { get; set; } // Giới tính

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Mặc định thời gian hiện tại

    public DateTime? UpdatedAt { get; set; } // Cập nhật lần cuối

    public bool IsVerified { get; set; } // Đã xác thực email chưa?

    public bool IsActive { get; set; } = true; // Tài khoản có bị khóa không?
}
