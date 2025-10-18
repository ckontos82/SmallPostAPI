namespace SmallPostAPI.DTOs
{
    public sealed record CreatePostDto(int UserId, string Title, string Body);
    public sealed record UpdatePostDto(string Title, string Body);

    public sealed record PostDto(int Id, int UserId, string Title, string Body);

    public sealed record PostWithUserDto(
        int Id, int UserId, string Title, string Body,
        UserMiniDto User);

    public sealed record UserMiniDto(int Id, string Name);
}
