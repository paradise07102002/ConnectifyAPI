using Connectify.Data;
using Connectify.Models;
using Microsoft.EntityFrameworkCore;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context; // DbContext object for interacting with the database

    // Constructor that receives AppDbContext and assigns it to the local variable _context
    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    // Retrieves a refresh token from the database, including the associated user
    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        return await _context.RefreshTokens.Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == token);
    }

    // Adds a new refresh token to the database
    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    // Checks if an email already exists in the system
    public async Task<bool> EmailExistsAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);

    // Adds a new user to the database (without saving immediately)
    public async Task AddUserAsync(User user)
    {
        if (user.Id == Guid.Empty)
        {
            user.Id = Guid.NewGuid();
        }
        // Assigns the user creation time (UTC)
        user.CreatedAt = DateTime.UtcNow;

        // Adds the user to the DbContext but does not save to the database yet
        await _context.Users.AddAsync(user);
    }

    // Removes a refresh token from the database
    public async Task RemoveRefreshTokenAsync(RefreshToken token)
    {
        _context.RefreshTokens.Remove(token);
        await _context.SaveChangesAsync();
    }

    // Saves changes to the database
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    // Retrieves user information based on email
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
