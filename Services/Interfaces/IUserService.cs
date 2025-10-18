using SmallPostAPI.DTOs;
using SmallPostAPI.Models;

namespace SmallPostAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetAsync(int id, CancellationToken ct = default);
        Task<UserWithPostsDto?> GetWithPostsAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default);
        Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default);
        Task UpdateAsync(int id, UpdateUserDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
