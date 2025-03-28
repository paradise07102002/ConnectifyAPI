using Connectify.Data;
using Connectify.Models;
using Microsoft.EntityFrameworkCore;

public class MediaRepository : IMediaRepository
{
    private readonly AppDbContext _context;

    public MediaRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<PostMedia>?> GetMediaByPostIdAsync(Guid postId)
    {
        return await _context.PostsMedias.Where(m  => m.PostId == postId).ToListAsync();
    }
}