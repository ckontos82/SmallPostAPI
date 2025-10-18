namespace SmallPostAPI.DTOs
{
    public sealed record CreateUserDto(string Name, string Email);
    public sealed record UpdateUserDto(string Name, string Email);

    public sealed record UserDto(int Id, string Name, string Email);

    public sealed record UserWithPostsDto(
        int Id, string Name, string Email,
        IReadOnlyList<PostUserDto> Posts);

    public sealed record PostUserDto(int Id, string Title, string Body);
}
