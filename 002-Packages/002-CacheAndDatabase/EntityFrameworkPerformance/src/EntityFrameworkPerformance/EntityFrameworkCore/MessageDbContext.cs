using EntityFrameworkPerformance.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkPerformance.EntityFrameworkCore
{
    public class MessageDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageInbox> MessageInboies { get; set; }


        private readonly DatabaseTypes _datebaseType;
        private readonly string _connectionString;

        public MessageDbContext(DatabaseTypes databaseType, string connectionString)
        {
            _datebaseType = databaseType;
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                switch (_datebaseType)
                {
                    case DatabaseTypes.InMemory:
                        optionsBuilder.UseInMemoryDatabase(_connectionString);
                        break;

                    case DatabaseTypes.SqlServer:
                        optionsBuilder.UseSqlServer(_connectionString);
                        break;

                    case DatabaseTypes.Postgres:
                        optionsBuilder.UseNpgsql(_connectionString);
                        break;

                    case DatabaseTypes.Sqlite:
                        optionsBuilder.UseSqlite(_connectionString);
                        break;

                    case DatabaseTypes.MySql:
                        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
                        break;
                }
            }

            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(o => o.Id);
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
