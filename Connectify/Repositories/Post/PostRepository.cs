using Connectify.Data;
using Connectify.Models;
using Microsoft.EntityFrameworkCore;

public class PostRepository : IPostRepository
{
    private readonly AppDbContext _context;

    public PostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> GetPostByIdAsync(Guid postId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Medias)
            .FirstOrDefaultAsync(p => p.Id == postId);
    }

    public async Task<List<Post>> GetAllPostAsync()
    {
        return await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Medias)
            .Include(p => p.Comments)
            .Include(p => p.Likes)
            .Include(p => p.shares)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}