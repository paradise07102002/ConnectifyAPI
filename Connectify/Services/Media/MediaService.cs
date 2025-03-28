using Connectify.Models;

public class MediaService : IMediaService
{
    private readonly IMediaRepository _mediaRepository;
    public MediaService(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }
    public async Task<List<PostMedia>?> GetMediaByPostIdAsync(Guid postId)
    {
        return await _mediaRepository.GetMediaByPostIdAsync(postId);
    }
}