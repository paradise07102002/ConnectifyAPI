using Connectify.Models;

public interface IPostService
{
    Task<Post> CreatePostAsync(CreatePostDto createPostDto, string userId);
    Task<Post?> GetPostByIdAsync(Guid postId);
    Task<List<Post>> GetAllPostAsync();
}