using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.Controllers
{
    [Route("api/posts/{postId}/comments")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto, string postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var comment = await _commentService.CreateCommentAsync(dto, userId, postId);
            return Ok(new { message = "Comment successful" });
        }

        [HttpGet] 
        public async Task<IActionResult> GetComments(Guid postId) 
        { 
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            } 
            var comments = await _commentService.GetCommentsAsync(postId); 

            if (comments == null) 
            { 
                return NotFound(new { message = "Post not found" }); 
            } 
            return Ok(comments); 
        }
    }
}
