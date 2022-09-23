using BookItem = Book.API.Model.Book;

namespace Book.API.Infrastructure;

public static class BookContextSeed
{
    public static async Task SeedAsync(this BookContext context)
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        if (!context.Books.Any())
        {
            await AddBookData(context);
        }

    }

    private static async Task AddBookData(BookContext context)
    {
        var lang = new BookLanguage { Title = "English" };
        await context.BookLanguages.AddAsync(lang);
        var oreilly = new BookPublisher { Title = "O'Reilly" };
        var apress = new BookPublisher { Title = "Apress" };
        var packt = new BookPublisher { Title = "Packt" };
        var addisonWesley = new BookPublisher { Title = "Addison-Wesley" };
        var pearson = new BookPublisher { Title = "Pearson" };
        await context.BookPublishers.AddRangeAsync(oreilly, apress, packt, addisonWesley, pearson);
        await context.SaveChangesAsync();
        await context.Books.AddRangeAsync(new BookItem
        {
            BookPublisherId = oreilly.Id,
            BookAuthor = new BookAuthor { FirstName = "Sam", LastName = "Newman", Description = "" },
            BookLanguageId = lang.Id,
            Title = "Building Microservices: Designing Fine-Grained Systems 2nd Edition",
            ISBN = "1492034029",
            Pages = 612,
            TotalCopies = 30,
            Weight = 2.1,
            Description = ""

        },
            new BookItem
            {
                BookPublisherId = oreilly.Id,
                BookAuthor = new BookAuthor { FirstName = "Irakli", LastName = "Nadareishvili", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Microservice Architecture: Aligning Principles, Practices, and Culture",
                ISBN = "1492034029",
                Pages = 146,
                TotalCopies = 30,
                Weight = 1,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = oreilly.Id,
                BookAuthor = new BookAuthor { FirstName = "Gaurav", LastName = "Raje", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Security and Microservice Architecture on AWS",
                ISBN = "1098101464",
                Pages = 396,
                TotalCopies = 30,
                Weight = 1.5,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = oreilly.Id,
                BookAuthor = new BookAuthor { FirstName = "Adam", LastName = "Bellemare", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Building Event-Driven Microservices: Leveraging Organizational Data at Scale",
                ISBN = "1098151464",
                Pages = 324,
                TotalCopies = 30,
                Weight = 1.4,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = addisonWesley.Id,
                BookAuthor = new BookAuthor { FirstName = "Eric", LastName = "Evans", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Domain-Driven Design: Tackling Complexity in the Heart of Software",
                ISBN = "1098141464",
                Pages = 560,
                TotalCopies = 30,
                Weight = 2.33,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = oreilly.Id,
                BookAuthor = new BookAuthor { FirstName = "Vlad", LastName = "Khononov", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Learning Domain-Driven Design: Aligning Software Architecture and Business Strategy",
                ISBN = "1098100131",
                Pages = 340,
                TotalCopies = 30,
                BorrowedCopies = 5,
                Weight = 1.2,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = addisonWesley.Id,
                BookAuthor = new BookAuthor { FirstName = "Vaughn", LastName = "Vernon", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Domain-Driven Design Distilled 1st Edition",
                ISBN = "0134434420",
                Pages = 176,
                TotalCopies = 30,
                BorrowedCopies = 3,
                Weight = 0.9,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = packt.Id,
                BookAuthor = new BookAuthor { FirstName = "Mithun", LastName = "Pattankar", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Mastering ASP.NET Web API: Build powerful HTTP services and make the most of the ASP.NET Core Web API platform",
                ISBN = "1786463954",
                Pages = 330,
                TotalCopies = 30,
                BorrowedCopies = 2,
                Weight = 1.25,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = packt.Id,
                BookAuthor = new BookAuthor { FirstName = "Samuele", LastName = "Resca", Description = "" },
                BookLanguageId = lang.Id,
                Title = "Hands-On RESTful Web Services with ASP.NET Core 3",
                ISBN = "1789537614",
                Pages = 510,
                TotalCopies = 30,
                BorrowedCopies = 2,
                Weight = 1.91,
                Description = ""

            },
            new BookItem
            {
                BookPublisherId = oreilly.Id,
                BookAuthor = new BookAuthor { FirstName = "Anibal", LastName = "Marcano", Description = "" },
                BookLanguageId = lang.Id,
                Title = "PROGRAMMING ASP.NET CORE 5 MVC AND WEB API: Examples in C#",
                ISBN = "1789437614",
                Pages = 372,
                TotalCopies = 30,
                BorrowedCopies = 1,
                Weight = 1.64,
                Description = ""

            });
        await context.SaveChangesAsync();
    }
}