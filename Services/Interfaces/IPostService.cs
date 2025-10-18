using Microsoft.Extensions.Hosting;
using SmallPostAPI.DTOs;
using SmallPostAPI.Models;

namespace SmallPostAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<PostDto?> GetAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<PostDto>> GetByUserAsync(int userId, CancellationToken ct = default);
        Task<PostDto> CreateAsync(CreatePostDto dto, CancellationToken ct = default);
        Task UpdateAsync(int id, UpdatePostDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
    }
}
