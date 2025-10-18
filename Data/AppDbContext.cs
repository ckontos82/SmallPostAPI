using SmallPostAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SmallPostAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();

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
        }
    }
}
