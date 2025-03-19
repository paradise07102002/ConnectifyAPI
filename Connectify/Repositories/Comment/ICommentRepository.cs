using Connectify.Models;

public interface ICommentRepository
{
    Task<Comment> AddCommentAsync(Comment comment);
}