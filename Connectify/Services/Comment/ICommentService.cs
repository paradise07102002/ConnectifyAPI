using Connectify.Models;

public interface ICommentService
{
    Task<Comment> CreateCommentAsync(CreateCommentDto dto, string userId, string postId);
}