using Connectify.Data;
using Connectify.Models;
using Microsoft.EntityFrameworkCore;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context; // Đối tượng DbContext để tương tác với database

    // Constructor nhận vào AppDbContext và gán vào biến cục bộ _context
    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    // Kiểm tra xem email đã tồn tại trong hệ thống hay chưa
    public async Task<bool> EmailExistsAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

    // Thêm một người dùng mới vào database (chưa lưu ngay)
    public async Task AddUserAsync(User user)
    {
        // Gán thời gian tạo người dùng (UTC)
        user.CreatedAt = DateTime.UtcNow;

        // Thêm user vào DbContext nhưng chưa lưu vào database
        await _context.Users.AddAsync(user);
    }

    // Lưu các thay đổi vào database
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    // Lấy thông tin người dùng dựa trên mã xác minh (Verification Token)
    public async Task<User?> GetUserByTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
    }

    // Lấy thông tin người dùng dựa trên email
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }


    //Task<bool> EmailExistsAsync(string email);
    //Task AddUserAsync(User user);
    //Task<User?> GetUserByTokenAsync(string token);
    //Task<User?> GetUserByEmailAsync(string email);
    //Task SaveChangesAsync();
}