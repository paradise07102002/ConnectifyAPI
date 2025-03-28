using Connectify.Models;

public interface IMediaRepository
{
    Task<List<PostMedia>?> GetMediaByPostIdAsync(Guid postId);
}