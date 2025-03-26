using Connectify.Models;

public interface ICommentRepository
{
    Task<Comment> CreateCommentAsync(Comment comment);
}