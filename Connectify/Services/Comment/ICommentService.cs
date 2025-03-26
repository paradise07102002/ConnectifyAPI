using Connectify.Models;

public interface ICommentService
{
    Task<Comment> AddCommentAsync(CreateCommentDto dto, string userId, string postId);
}