using EfCoreSample.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSample.EntityFrameworkCore
{
    public class IMDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageInbox> MessageInboies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Dev;Trusted_Connection=True");
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasKey(o => o.Id);

            modelBuilder.Entity<Group>()
                        .HasKey(o => o.Id);

            modelBuilder.Entity<UserGroup>(b =>
            {
                b.ToTable("UserGroups");
                b.HasKey(o => new { o.UserId, o.GroupId });
            });

            modelBuilder.Entity<Message>()
                        .HasKey(o => o.Id);
            
            modelBuilder.Entity<MessageInbox>(b =>
            {
                b.ToTable("MessageInbox");
                b.HasKey(o => new { o.MessageId, o.UserId });
            });
        }
    }
}
