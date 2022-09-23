using Borrow.Core.Entities;
using Borrow.Infrastructure.Persistence;
using Borrow.Infrastructure.Persistence.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace Borrow.UnitTests.Infrastructure;

public class BorrowRepositoryTests
{
    private readonly BorrowRepository _borrowRepository;

    public BorrowRepositoryTests()
    {
        var dbOptions = InMemoryDatabaseOptionCreator.CreateNewContextOptions();
        var dbContext = new BorrowContext(dbOptions);
        _borrowRepository = new BorrowRepository(dbContext);
        dbContext.BorrowedBooks.Add(new BorrowedBook()
        { BookId = 100, BorrowedDate = DateTime.UtcNow.AddDays(-10), UserId = 1 });
        dbContext.BorrowedBooks.Add(new BorrowedBook()
        { BookId = 200, BorrowedDate = DateTime.UtcNow.AddDays(-1), UserId = 1 });
        dbContext.BorrowedBooks.Add(new BorrowedBook()
        { BookId = 200, BorrowedDate = DateTime.UtcNow.AddDays(-5), UserId = 2 });
        dbContext.BorrowedBooks.Add(new BorrowedBook()
        { BookId = 220, BorrowedDate = DateTime.UtcNow.AddDays(-6), ReturnDate = DateTime.UtcNow.AddDays(-1), UserId = 2 });
        dbContext.SaveChanges();
    }
    [Fact]
    public async Task ShouldReturnCorrectGetBorrowedBooksByUserIdAsync()
    {
        var result = await _borrowRepository.GetBorrowedBooksByUserIdAsync(1);
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
        Assert.Contains(100, result.Select(x => x.BookId));

    }

    [Fact]
    public async Task ShouldReturnCorrectReturnedBooksByBookIdAsync()
    {
        var emptyList = await _borrowRepository.GetAllReturnedBooksByBookIdAsync(200);
        var singlList = await _borrowRepository.GetAllReturnedBooksByBookIdAsync(220);
        Assert.NotNull(emptyList);
        emptyList.ShouldNotBeNull();
        emptyList.ShouldBeEmpty();
        Assert.Single(singlList);
        singlList.FirstOrDefault()!.ReturnDate.ShouldNotBeNull();
    }

    [Fact]
    public async Task ShouldReturnCorrectGetAllBorrowedBookById()
    {
        var result = await _borrowRepository.GetAllBorrowedByBookIdAsync(200);
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
        Assert.Contains(200, result.Select(x => x.BookId));

    }
}