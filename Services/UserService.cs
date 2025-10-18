using Microsoft.EntityFrameworkCore;
using SmallPostAPI.Data;
using SmallPostAPI.DTOs;
using SmallPostAPI.Models;
using SmallPostAPI.Services.Interfaces;

namespace SmallPostAPI.Services
{
    public class UserService(AppDbContext db) : IUserService
    {
        public async Task<UserDto?> GetAsync(int id, CancellationToken ct = default)
        {
            return await db.Users
                .Where(u => u.Id == id)
                .Select(u => new UserDto(u.Id, u.Name, u.Email))
                .FirstOrDefaultAsync(ct);
        }

        public async Task<UserWithPostsDto?> GetWithPostsAsync(int id, CancellationToken ct = default)
        {
            return await db.Users
                .Where(u => u.Id == id)
                .Select(u => new UserWithPostsDto(
                    u.Id, u.Name, u.Email,
                    u.Posts
                     .OrderByDescending(p => p.Id)
                     .Select(p => new PostSummaryDto(p.Id, p.Title))
                     .ToList()))
                .FirstOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await db.Users
                .OrderBy(u => u.Id)
                .Select(u => new UserDto(u.Id, u.Name, u.Email))
                .ToListAsync(ct);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default)
        {
            if (await db.Users.AnyAsync(u => u.Email == dto.Email, ct))
                throw new InvalidOperationException("Email already exists.");

            var user = new User { Name = dto.Name, Email = dto.Email };
            db.Users.Add(user);
            await db.SaveChangesAsync(ct);
            return new UserDto(user.Id, user.Name, user.Email);
        }

        public async Task UpdateAsync(int id, UpdateUserDto dto, CancellationToken ct = default)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id, ct)
                       ?? throw new KeyNotFoundException("User not found.");

            user.Name = dto.Name;
            user.Email = dto.Email;
            await db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
            if (user is null) return;

            db.Users.Remove(user); 
            await db.SaveChangesAsync(ct);
        }
    }
}
