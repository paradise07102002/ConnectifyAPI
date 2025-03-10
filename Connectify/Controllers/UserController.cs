using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize] //Allow only logged in users to access this API
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
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
}