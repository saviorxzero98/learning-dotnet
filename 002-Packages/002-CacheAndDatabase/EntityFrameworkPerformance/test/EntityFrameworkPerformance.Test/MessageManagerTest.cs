using EntityFrameworkPerformance.Entities;
using EntityFrameworkPerformance.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkPerformance.Test
{
    public class MessageManagerTest
    {
        protected readonly IConfiguration Configuration;

        public MessageManagerTest()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                      .Build();
        }

        protected MessageDbContext CreateDbContext()
        {
            return new MessageDbContext(DatabaseTypes.SqlServer, Configuration.GetConnectionString("Default"));
        }

        [Fact]
        public async Task AddMessagesAsync()
        {
            using (var context = CreateDbContext())
            {
                for (var i = 0; i < 10; i++)
                {
                    var now = DateTime.Now;
                    var messageId = Guid.NewGuid();
                    var subject = $"Hello #{i + 1}";
                    var message = new Message()
                    {
                        Id = messageId,
                        Subject = subject,
                        Content = $"Hello World #{i + 1}",
                        SendTime = now,
                        IsDeleted = false
                    };

                    await context.Messages.AddAsync(message);
                    var users = await context.Users
                                             .AsNoTracking()
                                             .Where(u => u.IsActived)
                                             .ToListAsync();

                    foreach (var user in users)
                    {
                        var messageInbox = new MessageInbox()
                        {
                            MessageId = messageId,
                            Subject = subject,
                            UserId = user.Id,
                            IsAlreadyRead = false,
                            IsDeleted = false,
                            SendTime = now,
                        };

                        await context.MessageInboies.AddAsync(messageInbox);
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
