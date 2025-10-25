using Microsoft.AspNetCore.Mvc;
using SmallPostAPI.DTOs;
using SmallPostAPI.Services.Interfaces;

namespace SmallPostAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAll(CancellationToken ct)
        => Ok(await userService.GetAllAsync(ct));
        
        [HttpGet("useronly/{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await userService.GetAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserWithPostsDto>> Get(int id, CancellationToken ct)
        {
            var user = await userService.GetWithPostsAsync(id, ct);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            try
            {
                var created = await userService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto, CancellationToken ct)
        {
            try
            {
                await userService.UpdateAsync(id, dto, ct);
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
            await userService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
