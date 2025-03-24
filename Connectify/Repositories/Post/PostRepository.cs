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

    public async Task<List<PostDto>> GetAllPostAsync()
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Include(p => p.Medias)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Include(p => p.Likes)
            .Include(p => p.shares)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return posts.Select(p => new PostDto
        {
            Id = p.Id,
            Content = p.Content,
            PrivacyLevel = p.PrivacyLevel,
            CreatedAt = p.CreatedAt,
            UpdateAt = p.UpdateAt,
            userId = p.userId,
            AvatarUrl = p.User.AvatarUrl,
            FullName = p.User.FullName,
            Medias = p.Medias,
            Comments = p.Comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Content = c.Content,
                CreateAt = c.CreateAt,
                UserId = c.UserId,
                fullName = c.User.FullName,
                avatarUrl = c.User.AvatarUrl,
            }).ToList(),
            Likes = p.Likes,
            shares = p.shares,
        }).ToList();
    }
}