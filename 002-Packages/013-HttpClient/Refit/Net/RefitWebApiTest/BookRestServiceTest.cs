using RefitWebApiCore.RestServices;
using RefitWebApiCore.Models;
using RefitWebApiCore.Models.Books;

namespace RefitWebApiTest
{
    public class BookRestServiceTest : IClassFixture<ContainerFixture<IBookRestService>>
    {
        private readonly IBookRestService _appService;

        public BookRestServiceTest(ContainerFixture<IBookRestService> container)
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
            var book = await _appService.AddAsync(new AddBookRequest()
            {
                Name = "Alice's Adventures in Wonderland",
                Episode = 1,
                Price = 500,
                Author = new Author()
                {
                    Uid = 1,
                    Name = "Charles Lutwidge Dodgson"
                },
                PublishDate = new DateTime(1865, 7, 4),
                IsReprint = false
            });

            // Assert
            Assert.NotNull(book);
        }
    }
}
