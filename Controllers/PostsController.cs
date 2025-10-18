using Microsoft.AspNetCore.Mvc;
using SmallPostAPI.DTOs;
using SmallPostAPI.Models;
using SmallPostAPI.Services.Interfaces;

namespace SmallPostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostService postService) : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDto>> Get(int id, CancellationToken ct)
        {
            var post = await postService.GetAsync(id, ct);
            return post is null ? NotFound() : Ok(post);
        }

        [HttpGet("by-user/{userId:int}")]
        public async Task<ActionResult<IReadOnlyList<PostDto>>> GetByUser(int userId, CancellationToken ct)
            => Ok(await postService.GetByUserAsync(userId, ct));

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromBody] CreatePostDto dto, CancellationToken ct)
        {
            var created = await postService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePostDto dto, CancellationToken ct)
        {
            await postService.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await postService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
