using Microsoft.EntityFrameworkCore;
using SmallPostAPI.Data;
using SmallPostAPI.DTOs;
using SmallPostAPI.Mapping;
using SmallPostAPI.Models;
using SmallPostAPI.Services.Interfaces;

namespace SmallPostAPI.Services
{
    public class PostService(AppDbContext db) : IPostService
    {
        public Task<PostDto?> GetAsync(int id, CancellationToken ct = default)
        => db.Posts
              .Where(p => p.Id == id)
              .Select(p => new PostDto(p.Id, p.UserId, p.Title, p.Body))
              .FirstOrDefaultAsync(ct);

        public async Task<IReadOnlyList<PostDto>> GetByUserAsync(int userId, CancellationToken ct = default)
        {
            var postsDto = await db.Posts
                            .Where(p => p.UserId == userId)
                            .OrderByDescending(p => p.Id)
                            .Select(p => p.ToDto())
                            .ToListAsync(ct);

            return postsDto.AsReadOnly();
        }

        public async Task<PostDto> CreateAsync(CreatePostDto dto, CancellationToken ct = default)
        {
            var userExists = await db.Users.AnyAsync(u => u.Id == dto.UserId, ct);
            if (!userExists) 
                throw new KeyNotFoundException("User not found.");

            var post = dto.ToPost();
            db.Posts.Add(post);
            await db.SaveChangesAsync(ct);

            return new PostDto(post.Id, post.UserId, post.Title, post.Body);
        }

        public async Task UpdateAsync(int id, UpdatePostDto dto, CancellationToken ct = default)
        {
            var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id, ct)
                       ?? throw new KeyNotFoundException("Post not found.");

            post.Title = dto.Title;
            post.Body = dto.Body;
            await db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var post = await db.Posts.FirstOrDefaultAsync(p => p.Id == id, ct);
            if (post is null) return;

            db.Posts.Remove(post);
            await db.SaveChangesAsync(ct);
        }
    }
}
