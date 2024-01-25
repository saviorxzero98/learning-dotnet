using RefitWebApiCore.AppServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;

namespace RefitWebApiTest
{
    public class UserAppServiceTest : IClassFixture<ContainerFixture<IUserAppService>>
    {
        private readonly IUserAppService _appService;

        public UserAppServiceTest(ContainerFixture<IUserAppService> container)
        {
            _appService = container.AppService;
        }

        [Fact]
        public async Task Test_GetAsync()
        {
            // Act
            var book = await _appService.GetAsync(Guid.NewGuid().ToString());

            // Assert
            Assert.NotNull(book);
        }


        [Fact]
        public async Task Test_GetListAsync()
        {
            // Act
            var bookList = await _appService.GetListAsync(new PageDataQuery());

            // Assert
            Assert.NotEmpty(bookList);
        }

        [Fact]
        public async Task Test_AddAsync()
        {
            // Act
            var book = await _appService.AddAsync(new AddUserRequest()
            {
                Name = "Iota",
                IsActived = true,
                CreateDate = DateTime.Now
            });

            // Assert
            Assert.NotNull(book);
        }
    }
}
