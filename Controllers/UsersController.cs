using Microsoft.AspNetCore.Mvc;
using SmallPostAPI.DTOs;
using SmallPostAPI.Models;
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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserWithPostsDto>> Get(int id, CancellationToken ct)
        {
            var user = await userService.GetWithPostsAsync(id, ct);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            var created = await userService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto, CancellationToken ct)
        {
            await userService.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            await userService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}
