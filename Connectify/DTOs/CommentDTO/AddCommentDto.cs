public class AddCommentDto
{
    public string Content { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;

}