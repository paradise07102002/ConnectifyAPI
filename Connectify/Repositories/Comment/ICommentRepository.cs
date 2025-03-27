using Connectify.Models;

public interface ICommentRepository
{
    Task<Comment> CreateCommentAsync(Comment comment);
    Task<List<CommentDto>> GetCommentsAsync(Guid postId);
}