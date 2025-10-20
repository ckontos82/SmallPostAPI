namespace SmallPostAPI.Models
{
    public enum FriendshipStatus
    {
        Pending,
        Accepted,
        Declined,
        Blocked
    }

    public class Friendship
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public int RequestedByUserId { get; set; }
        public FriendshipStatus Status { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; } = null!;
        public User Friend { get; set; } = null!;
        public User RequestedByUser { get; set; } = null!;

    }
}
