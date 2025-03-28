using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.Controllers
{
    [Route("api/posts/{postId}/medias")]
    [ApiController]
    [Authorize]
    public class MediasController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        public MediasController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMediaByPostId(Guid postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medias = await _mediaService.GetMediaByPostIdAsync(postId);

            if (medias == null)
            {
                return NotFound(new { message = "No medias"});
            }

            return Ok(medias);
        }
    }
}
