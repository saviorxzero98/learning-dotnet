using EFCoreConsole.Contexts;
using EFCoreConsole.Entities;

namespace EFCoreConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=BookStore.db;";

            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Name = "BookA",
                Description = "Description",
                Author = "Author",
                Price = 500,
                HasStock = true,
                CreateDate = DateTime.Now
            };

            using (var dbContext = new BookContext(connectionString))
            {
                dbContext.Add(book);

                dbContext.SaveChanges();

                var insertedBook = dbContext.Books.Where(b => b.Id == book.Id).FirstOrDefault();

                insertedBook.HasStock = false;
                

                dbContext.Update(book);
                dbContext.SaveChanges();
            }
        }
    }
}