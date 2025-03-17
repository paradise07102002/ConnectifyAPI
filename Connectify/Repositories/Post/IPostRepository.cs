using Connectify.Models;

public interface IPostRepository
{
    Task<Post> CreatePostAsync(Post post);
    Task<Post?> GetPostByIdAsync(Guid postId);
    Task<List<Post>> GetAllPostAsync();
}