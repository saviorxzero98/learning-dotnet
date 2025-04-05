using EfCoreSample.Entities;
using EfCoreSample.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfCoreSampleTest.TestFixtures
{
    public class DatabaseTestFixture : BaseTestFixture
    {
        public override void OnInitialize(
            IConfiguration config,
            IServiceProvider services)
        {
            //using var dbContext = new IMDbContext();

            //dbContext.Users.Add(new User("002", "Lyrae"));
            //dbContext.Users.Add(new User("003", "Altair"));
            //dbContext.Users.Add(new User("004", "Deneb"));
            //dbContext.Users.Add(new User("001", "Ace"));
            //dbContext.Users.Add(new User("011", "Jack"));
            //dbContext.Users.Add(new User("012", "Queen"));
            //dbContext.Users.Add(new User("013", "King"));
            //dbContext.Users.Add(new User("014", "Joker"));

            //dbContext.Groups.Add(new Group("000", "Default"));
            //dbContext.Groups.Add(new Group("001", "Poke"));

            //dbContext.UserGroups.Add(new UserGroup("001", "001"));
            //dbContext.UserGroups.Add(new UserGroup("011", "001"));
            //dbContext.UserGroups.Add(new UserGroup("012", "001"));
            //dbContext.UserGroups.Add(new UserGroup("013", "001"));
            //dbContext.UserGroups.Add(new UserGroup("014", "001"));

            //dbContext.SaveChanges();
        }

        public override void Dispose()
        {
            //using var dbContext = new IMDbContext();

            //dbContext.Users.ExecuteDelete();
            //dbContext.Groups.ExecuteDelete();
            //dbContext.UserGroups.ExecuteDelete();
        }
    }
}
