using RefitWebApiCore.RestServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Users;

namespace RefitWebApiTest
{
    public class UserRestServiceTest : IClassFixture<ContainerFixture<IUserRestService>>
    {
        private readonly IUserRestService _appService;

        public UserRestServiceTest(ContainerFixture<IUserRestService> container)
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
