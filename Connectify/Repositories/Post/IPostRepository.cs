using Connectify.Models;

public interface IPostRepository
{
    Task<Post> CreatePostAsync(Post post);
}