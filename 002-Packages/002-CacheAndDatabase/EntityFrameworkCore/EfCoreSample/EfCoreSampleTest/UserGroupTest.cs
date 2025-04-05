using EfCoreSample.Entities;
using EfCoreSample.EntityFrameworkCore;
using EfCoreSampleTest.TestFixtures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfCoreSampleTest
{
    public class UserGroupTest : IClassFixture<DatabaseTestFixture>
    {
        public IConfiguration Configuration { get; set; }
        public IServiceProvider ServiceProvider { get; set; }

        public UserGroupTest(DatabaseTestFixture fixture)
        {
            Configuration = fixture.Configuration;
            ServiceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public async Task TestWithForEach()
        {
            using var dbContext = CreateDbContext();

            var groups = await dbContext.Groups.ToListAsync();

            foreach (var group in groups)
            {
                if (group == null)
                {
                    continue;
                }

                var groupUserIds = await dbContext.UserGroups.Where(r => r.GroupId == group.Id)
                                                             .Select(r => r.UserId)
                                                             .ToListAsync();

                var members = new List<User>();
                foreach (var userId in groupUserIds)
                {
                    var users = await dbContext.Users.Where(u => userId == u.Id)
                                                     .ToListAsync();
                }
                group.Members = members;
            }
        }


        [Fact]
        public async Task TestWithContains()
        {
            using var dbContext = CreateDbContext();

            var group = await dbContext.Groups.FirstOrDefaultAsync(g => g.Name == "Poke");
            if (group == null)
            {
                return;
            }

            var groupUsers = await dbContext.UserGroups.Where(r => r.GroupId == group.Id)
                                                       .Select(r => r.UserId)
                                                       .ToListAsync();

            var users = await dbContext.Users.Where(u => groupUsers.Contains(u.Id))
                                             .ToListAsync();

            group.Members = users;
        }

        private IMDbContext CreateDbContext()
        {
            return new IMDbContext();
        }
    }
}
