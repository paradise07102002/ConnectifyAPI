using Connectify.Models;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> AddCommentAsync(AddCommentDto dto, string userId, string postId)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = dto.Content,
            CreateAt = dto.CreateAt,
            PostId = Guid.Parse(postId),
            UserId = Guid.Parse(userId),
        };

        return await _commentRepository.AddCommentAsync(comment);
    }
}