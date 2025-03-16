using Connectify.Models;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly GoogleCloudStorageService _storageService;

    public PostService(IPostRepository postRepository, GoogleCloudStorageService storageService)
    {
        _postRepository = postRepository;
        _storageService = storageService;
    }

    public async Task<Post> CreatePostAsync(CreatePostDto createPostDto, string userId)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            userId = Guid.Parse(userId),
            Content = createPostDto.Content,
            PrivacyLevel = createPostDto.PrivacyLevel,
            Medias = new List<PostMedia>(),
            CreatedAt = DateTime.UtcNow,
        };

        if (createPostDto.Files != null && createPostDto.Files.Count > 0)
        {
            foreach (var file in createPostDto.Files)
            {
                var storageObject = await _storageService.UploadFileToGCS(file, userId);

                post.Medias.Add(new PostMedia
                {
                    MediaUrl = storageObject.MediaLink,
                    Type = file.ContentType.StartsWith("image") ? MediaType.Image : MediaType.Video
                });
            }
        }

        return await _postRepository.CreatePostAsync(post);

    }
}