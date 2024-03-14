using YesSql;
using YesSql.Provider.Sqlite;

namespace YesSQLSample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var store = await StoreFactory.CreateAndInitializeAsync(
                                   new Configuration()
                                     .UseSqLite(@"Data Source=yessql.db;Cache=Shared")
                                     .SetTablePrefix("Doc"));


            // creating a blog post
            var post = new BlogPost
            {
                Title = "Hello YesSql",
                Author = "Bill",
                Content = "Hello",
                PublishedUtc = DateTime.UtcNow,
                Tags = new[] { "Hello", "YesSql" }
            };

            // saving the post to the database
            await using (var session = store.CreateSession())
            {
                session.Save(post);

                await session.SaveChangesAsync();
            }
        }
    }
}