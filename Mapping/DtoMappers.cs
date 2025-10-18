using SmallPostAPI.DTOs;
using SmallPostAPI.Models;

namespace SmallPostAPI.Mapping
{
    public static class DtoMappers
    {
        public static UserDto ToDto(this User u)
        => new(u.Id, u.Name, u.Email);

        public static UserWithPostsDto ToWithPostsDto(this User u)
            => new(u.Id, u.Name, u.Email, u.Posts.Select(p => new PostUserDto(p.Id, p.Title, p.Body)).ToList());

        public static PostDto ToDto(this Post p)
            => new(p.Id, p.UserId, p.Title, p.Body);
    }
}
