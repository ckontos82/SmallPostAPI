using SmallPostAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmallPostAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Friendship> Friendships => Set<Friendship>();


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasKey(u => u.Id);
                b.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                b.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Post>(b =>
            {
                b.ToTable("Posts");
                b.HasKey(p => p.Id);

                b.Property(p => p.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                b.Property(p => p.Body)
                    .IsRequired();


                b.HasOne(p => p.User)
                    .WithMany(u => u.Posts)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.ToTable("Friendships", t =>
                {
                    t.HasCheckConstraint(
                        "CK_Friendships_UnorderedPair",
                        "[UserId] < [FriendId]"
                    );
                });

                b.HasKey(x => new { x.UserId, x.FriendId });

                b.HasOne(x => x.User)
                 .WithMany()
                 .HasForeignKey(x => x.UserId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.Friend)
                 .WithMany()
                 .HasForeignKey(x => x.FriendId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(x => x.RequestedByUser)
                 .WithMany()
                 .HasForeignKey(x => x.RequestedByUserId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => new { x.UserId, x.Status });
                b.HasIndex(x => new { x.FriendId, x.Status });
            });
        }
    }
}
