﻿using System.Diagnostics;
using System.Security.Claims;
using Connectify.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Connectify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto createPostDto)
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

            var post = await _postService.CreatePostAsync(createPostDto, userId);

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);

        }

        [HttpGet("{id}/get-post-by-id")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new { message = "Post not found" });
            } 

            return Ok(post);
                
        }

        [HttpGet("get-all-post")]
        public async Task<IActionResult> GetAllPost()
        {
            var posts = await _postService.GetAllPostAsync();

            return Ok(posts);
        }
    }
}
