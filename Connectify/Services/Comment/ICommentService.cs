using Connectify.Models;

public interface ICommentService
{
    Task<Comment> AddCommentAsync(AddCommentDto dto, string userId, string postId);
}