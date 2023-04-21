using EFCoreConsole.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConsole.Contexts
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        private string _connectionString;

        public BookContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_connectionString);
            }
        }
    }
}
