using Connectify.Models;

public interface IMediaService
{
    Task<List<PostMedia>?> GetMediaByPostIdAsync(Guid postId);
}