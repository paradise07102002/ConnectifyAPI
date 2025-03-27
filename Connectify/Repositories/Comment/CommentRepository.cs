using Connectify.Data;
using Connectify.Models;
using Microsoft.EntityFrameworkCore;

public class CommentRepository : ICommentRepository
{
    private readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
    
    public async Task<List<CommentDto>> GetCommentsAsync(Guid postId)
    {
        var comments = await _context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return comments.Select(c => new CommentDto()
        {
            Id = c.Id,
            Content = c.Content,
            CreatedAt = c.CreatedAt,
            PostId = c.PostId,
            UserId = c.UserId,
            avatarUrl = c.User.AvatarUrl,
            fullName = c.User.FullName,
        }).ToList();
    }
}