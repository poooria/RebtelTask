namespace Borrow.Infrastructure.Persistence;

public static class BorrowContextSeed
{
    public static async Task SeedAsync(this BorrowContext context)
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        var borrows =  context.BorrowedBooks;
        if (!borrows.Any())
        {
            await AddBorrows(context);
        }
    }

    public static async Task AddBorrows(BorrowContext context)
    {
        await context.BorrowedBooks.AddRangeAsync(new Core.Entities.BorrowedBook
            {
                BookId = 1,
                UserId = 1,
                BorrowedDate = DateTime.Parse("2022-02-06"),
                ReturnDate = DateTime.Parse("2022-02-12"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 1,
                UserId = 2,
                BorrowedDate = DateTime.Parse("2022-04-08"),
                ReturnDate = DateTime.Parse("2022-04-18"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 1,
                UserId = 3,
                BorrowedDate = DateTime.Parse("2022-04-02"),
                ReturnDate = DateTime.Parse("2022-04-20"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 1,
                UserId = 4,
                BorrowedDate = DateTime.Parse("2022-06-05"),
                ReturnDate = DateTime.Parse("2022-06-11"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 1,
                UserId = 5,
                BorrowedDate = DateTime.Parse("2022-06-05"),
                ReturnDate = DateTime.Parse("2022-06-12"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 2,
                UserId = 1,
                BorrowedDate = DateTime.Parse("2022-08-01"),
                ReturnDate = DateTime.Parse("2022-08-12"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 2,
                UserId = 2,
                BorrowedDate = DateTime.Parse("2022-06-01"),
                ReturnDate = DateTime.Parse("2022-06-09"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 2,
                UserId = 5,
                BorrowedDate = DateTime.Parse("2022-06-02"),
                ReturnDate = DateTime.Parse("2022-06-12"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 2,
                UserId = 6,
                BorrowedDate = DateTime.Parse("2022-06-02"),
                ReturnDate = DateTime.Parse("2022-06-10"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 3,
                UserId = 1,
                BorrowedDate = DateTime.Parse("2022-09-01"),
                ReturnDate = DateTime.Parse("2022-09-18"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 3,
                UserId = 3,
                BorrowedDate = DateTime.Parse("2022-09-01"),
                ReturnDate = DateTime.Parse("2022-09-18"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 3,
                UserId = 5,
                BorrowedDate = DateTime.Parse("2022-09-01"),
                ReturnDate = DateTime.Parse("2022-09-18"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 3,
                UserId = 6,
                BorrowedDate = DateTime.Parse("2022-09-01"),
                ReturnDate = DateTime.Parse("2022-09-18"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 4,
                UserId = 1,
                BorrowedDate = DateTime.Parse("2022-07-01"),
                ReturnDate = DateTime.Parse("2022-07-13"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 4,
                UserId = 2,
                BorrowedDate = DateTime.Parse("2022-07-01"),
                ReturnDate = DateTime.Parse("2022-07-15"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 4,
                UserId = 4,
                BorrowedDate = DateTime.Parse("2022-08-01"),
                ReturnDate = DateTime.Parse("2022-08-15"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 4,
                UserId = 1,
                BorrowedDate = DateTime.Parse("2022-06-04"),
                ReturnDate = DateTime.Parse("2022-06-15"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 5,
                UserId = 2,
                BorrowedDate = DateTime.Parse("2022-04-04"),
                ReturnDate = DateTime.Parse("2022-04-11"),
            }, new Core.Entities.BorrowedBook
            {
                BookId = 5,
                UserId = 3,
                BorrowedDate = DateTime.Parse("2022-04-08"),
                ReturnDate = DateTime.Parse("2022-04-15"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 5,
                UserId = 4,
                BorrowedDate = DateTime.Parse("2022-06-04"),
                ReturnDate = DateTime.Parse("2022-06-15"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 5,
                UserId = 5,
                BorrowedDate = DateTime.Parse("2022-07-04"),
                ReturnDate = DateTime.Parse("2022-07-12"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 5,
                UserId = 6,
                BorrowedDate = DateTime.Parse("2022-08-04"),
                ReturnDate = DateTime.Parse("2022-08-11"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 5,
                UserId = 7,
                BorrowedDate = DateTime.Parse("2022-06-04"),
                ReturnDate = DateTime.Parse("2022-06-15"),
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 6,
                UserId = 1,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 6,
                UserId = 2,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 6,
                UserId = 3,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 6,
                UserId = 4,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 6,
                UserId = 5,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 7,
                UserId = 1,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 7,
                UserId = 2,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 7,
                UserId = 4,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 8,
                UserId = 2,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 8,
                UserId = 3,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 9,
                UserId = 1,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 9,
                UserId = 2,
                BorrowedDate = DateTime.Now
            },
            new Core.Entities.BorrowedBook
            {
                BookId = 10,
                UserId = 4,
                BorrowedDate = DateTime.Now
            });
        await context.SaveChangesAsync();
    }
}