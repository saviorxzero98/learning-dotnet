using EntityFrameworkPerformance.Entities;
using EntityFrameworkPerformance.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EntityFrameworkPerformance.Test
{
    public class UserManagerTest
    {
        protected readonly IConfiguration Configuration;

        public UserManagerTest()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                                      .Build();
        }

        protected MessageDbContext CreateDbContext()
        {
            return new MessageDbContext(DatabaseTypes.SqlServer, Configuration.GetConnectionString("Default"));
        }

        [Fact]
        public async Task TestGetUsersAsync()
        {
            using (var context = CreateDbContext())
            {
                var startTime = DateTime.Now;

                var users = await context.Users
                                         .Where(u => u.IsActived)
                                         .ToListAsync();

                await context.SaveChangesAsync();

                var endTime = DateTime.Now;
                var totalTimes = (endTime - startTime).TotalMilliseconds;
            }
        }

        [Fact]
        public async Task TestGetUsersAsNoTrackingAsync()
        {
            using (var context = CreateDbContext())
            {
                var startTime = DateTime.Now;

                var users = await context.Users
                                         .AsNoTracking()
                                         .Where(u => u.IsActived)
                                         .ToListAsync();

                await context.SaveChangesAsync();

                var endTime = DateTime.Now;
                var totalTimes = (endTime - startTime).TotalMilliseconds;
            }
        }


        [Fact]
        public async Task TestAddUsersAsync()
        {
            using (var context = CreateDbContext())
            {
                //for (var i = 0; i < 100; i++)
                //{
                //    var user = new User()
                //    {
                //        Name = $"User-{i:D3}",
                //        IsActived = true
                //    };
                //    await context.Users.AddAsync(user);
                //}

                //await context.SaveChangesAsync();
            }
        }
    }
}
