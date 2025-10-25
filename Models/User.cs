namespace SmallPostAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Friendship> FriendshipsInitiated { get; } = new List<Friendship>();
        public ICollection<Friendship> FriendshipsReceived { get; } = new List<Friendship>();
    }
}
