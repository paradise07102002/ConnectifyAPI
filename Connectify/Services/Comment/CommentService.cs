using Connectify.Models;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> AddCommentAsync(CreateCommentDto dto, string userId, string postId)
    {
        var comment = new Comment
        {
            Content = dto.Content,
            PostId = Guid.Parse(postId),
            UserId = Guid.Parse(userId),
        };

        return await _commentRepository.AddCommentAsync(comment);
    }
}