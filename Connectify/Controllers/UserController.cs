using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize] //Allow only logged in users to access this API
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly GoogleCloudStorageService _storageService;

    public UserController(IUserService userService, GoogleCloudStorageService storageService)
    {
        _userService = userService;
        _storageService = storageService;
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userID))
        {
            return Unauthorized(new { message = "Invalid token" });
        } 

        var user = await _userService.GetUserByIdAsync(userID);

        if (user == null)
        {
            return NotFound(new {message = "User not found"});
        }
        return Ok(user);
            
    }

    [HttpPost("upload-avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("Please select the appropriate file");
            }

            var storage = await _storageService.UploadFileAsync(file, userId.ToString());

            string imageUrl = storage.MediaLink;

            await _userService.UpdateUserAvatarAsync(userId, imageUrl);

            return Ok(new { message = "Photo uploaded successfully", avatarUrl = imageUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Image loading error", error = ex.Message });
        } 
        
        
    }
}