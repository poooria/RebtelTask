using Shouldly;

namespace Book.UnitTests
{

    public class BookRepositoryTests
    {
        private readonly BookRepository _repository;
        public BookRepositoryTests()// Initialize the In memory database
        {
            var dbOptions = InMemoryDatabaseOptionCreator.CreateNewContextOptions();
            var dbContext = new BookContext(dbOptions);
            _repository = new BookRepository(dbContext);
            dbContext.Books.Add(new BookModel()
            {
                BookAuthor = new BookAuthor() { FirstName = "Poorya", LastName = "Ghajar", Description = "Microsoft .Net developer" },
                BookLanguage = new BookLanguage() { Title = "En" },
                Description = "Microservice Architecture",
                Title = "New approchaes on microservices",
                BookPublisher = new BookPublisher() { Title = "Microsoft" },
                BorrowedCopies = 5,
                ISBN = "fhdf656565660",
                Pages = 300,
                TotalCopies = 20,
                Weight = 50.0,
            });
            dbContext.Books.Add(new BookModel()
            {
                BookAuthor = new BookAuthor() { FirstName = "Robert", LastName = "Martin", Description = "Clean Man!" },
                BookLanguage = new BookLanguage() { Title = "En" },
                Description = "Clean Architecture",
                Title = "Guide To Clean Architecture",
                BookPublisher = new BookPublisher() { Title = "Microsoft" },
                BorrowedCopies = 8,
                ISBN = "54555121445845",
                Pages = 350,
                TotalCopies = 20,
                Weight = 60.0,
            });
            dbContext.SaveChanges();
        }
        [Fact]
        public async Task ShouldReturnBookById()
        {
            var result = await _repository.GetBookAsync(1);
            result.ShouldNotBeNull();
            result.BookAuthor.FirstName.ShouldBe("Poorya");
            result.Description.ShouldContain("Microservice");

        }
        [Fact]
        public async Task ShouldReturnBooksById()
        {
            var result = await _repository.GetBooksByIdsAsync(new[] { 1, 2 });
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }
    }
}