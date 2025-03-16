using Microsoft.AspNetCore.Http;

public class CreatePostDto
{
    public string Content { get; set; } = string.Empty;
    public PrivacyLevel PrivacyLevel { get; set; } = PrivacyLevel.Public;
    public List<IFormFile>? Files { get; set; }

}