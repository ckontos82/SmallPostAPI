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
            try
            {
                var created = await postService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePostDto dto, CancellationToken ct)
        {
            try
            {
                await postService.UpdateAsync(id, dto, ct);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);    
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await postService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
