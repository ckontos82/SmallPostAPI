using SmallPostAPI.DTOs;
using SmallPostAPI.Models;

namespace SmallPostAPI.Mapping
{
    public static class DtoMappers
    {
        public static UserDto ToDto(this User u)
        => new(u.Id, u.Name, u.Email);

        public static UserWithPostsDto ToUserWithPostsDto(this User u) =>
        new(
             u.Id, u.Name, u.Email,
             (u.Posts ?? Enumerable.Empty<Post>())
                .OrderByDescending(p => p.Id)
                .Select(p => new PostUserDto(p.Id, p.Title, p.Body))
                .ToList()
        );

        public static PostDto ToDto(this Post p)
            => new(p.Id, p.UserId, p.Title, p.Body);

        public static User ToUser(this CreateUserDto dto)
            => new()
            {
                Name = dto.Name,
                Email = dto.Email,
            };

        public static Post ToPost(this CreatePostDto dto)
            => new()
            {
               Title = dto.Title,
               Body = dto.Body,
               UserId = dto.UserId
            };
    }
}
