using EntityFrameworkPerformance.Entities;
using EntityFrameworkPerformance.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkPerformance.Test
{
    public class UserMessageManagerTest
    {
        protected readonly IConfiguration Configuration;


        public UserMessageManagerTest()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                      .Build();
        }

        protected MessageDbContext CreateDbContext()
        {
            return new MessageDbContext(DatabaseTypes.SqlServer, Configuration.GetConnectionString("Default"));
        }

        [Fact]
        public async Task TestGetUserMessage2Async()
        {
            using var context = CreateDbContext();

            var users = await context.Users
                                        .AsNoTracking()
                                        .Select(x => new User() { Id = x.Id, Name = x.Name })
                                        .Skip(0).Take(10)
                                        .ToListAsync();
        }


        [Fact]
        public async Task TestGetUserMessageAsync()
        {



            using (var context = CreateDbContext())
            {
                var startTime = DateTime.Now;

                var users = await context.Users
                                         .AsNoTracking()
                                         .Select(x => new User() { Id = x.Id, Name = x.Name })
                                         .Skip(0).Take(10)
                                         .ToListAsync();

                foreach (var user in users)
                {
                    var userMessages = await context.MessageInboies
                                                    .Where(i => i.IsDeleted == false)
                                                    .Where(i => i.UserId == user.Id)
                                                    .ToListAsync();

                    foreach (var userMessage in userMessages) 
                    {
                        var message = await context.Messages
                                                   .Where(i => i.IsDeleted == false)
                                                   .Where(i => i.Id == userMessage.MessageId)
                                                   .FirstOrDefaultAsync();
                        if (message != null)
                        {
                            user.UserMessages.Add(message);
                        }
                    }
                }

                var endTime = DateTime.Now;
                var totalTimes = (endTime - startTime).TotalMilliseconds;
            }
        }
    }
}
