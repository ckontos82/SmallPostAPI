using Microsoft.EntityFrameworkCore;
using SmallPostAPI.Data;
using SmallPostAPI.DTOs;
using SmallPostAPI.Mapping;
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
                .Select(u => u.ToDto())
                .FirstOrDefaultAsync(ct);
        }

        public async Task<UserWithPostsDto?> GetWithPostsAsync(int id, CancellationToken ct = default)
        {
            return await db.Users
                .Where(u => u.Id == id)
                .Include(u => u.Posts)
                .Select(u => u.ToUserWithPostsDto())
                .SingleOrDefaultAsync(ct);
        }

        public async Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default)
        {
            return await db.Users
                .OrderBy(u => u.Id)
                .Select(u => u.ToDto())
                .ToListAsync(ct);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken ct = default)
        {
            if (await db.Users.AnyAsync(u => u.Email == dto.Email, ct))
                throw new InvalidOperationException($"User with email {dto.Email} already exists.");

            var user = dto.ToUser();
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
