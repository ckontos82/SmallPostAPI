namespace SmallPostAPI.DTOs
{
    public sealed record CreateUserDto(string Name, string Email);
    public sealed record UpdateUserDto(string Name, string Email);

    public sealed record UserDto(int Id, string Name, string Email);

    public sealed record UserWithPostsDto(
        int Id, string Name, string Email,
        IReadOnlyList<PostSummaryDto> Posts);

    public sealed record PostSummaryDto(int Id, string Title);
}
