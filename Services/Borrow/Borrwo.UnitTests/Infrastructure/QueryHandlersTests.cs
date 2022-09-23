using System.Collections.Generic;
using System.Threading;
using Borrow.Application.Persistence;
using Borrow.Contracts.DTO;
using Borrow.Core.Entities;
using Borrow.Infrastructure.BorrowedBook.Queries;
using Borrow.Infrastructure.Persistence;
using Borrow.Infrastructure.Persistence.Repositories;
using Moq;
using Shouldly;

namespace Borrow.UnitTests.Infrastructure;

public class QueryHandlersTests
{
    private readonly BorrowRepository _borrowRepository;
    public QueryHandlersTests()
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
    public async Task GetMostBorrowedBooksHandlerTest()
    {
        //Arrange
        var getMostBorrowedBooksHandler = new GetMostBorrowedBooksHandler(_borrowRepository);
        //Act
        var result = await getMostBorrowedBooksHandler.Handle(new GetMostBorrowedBooksRequest(), new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.FirstOrDefault().BookId.ShouldBe(200);
    }
    [Fact]
    public async Task GetMostBorrowedBooksHandlerShouldCallRepository()
    {
        //Arrange
        var fakeRepository = new Mock<IBorrowRepository>();
        fakeRepository.Setup(x => x.GetAllBorrowed()).Returns(_borrowRepository.GetAllBorrowed);
        var getMostBorrowedBooksHandler = new GetMostBorrowedBooksHandler(fakeRepository.Object);
        //Act
        var result = await getMostBorrowedBooksHandler.Handle(new GetMostBorrowedBooksRequest(), new CancellationToken(false));
        //Assert
        fakeRepository.Verify(x => x.GetAllBorrowed(), Times.Once);
    }
    [Fact]
    public async Task GetBorrowedBooksByUserHandlerTest()
    {
        //Arrange
        var getMostBorrowedBooksHandler = new GetBorrowedBooksByUserHandler(_borrowRepository);
        //Act
        var result = await getMostBorrowedBooksHandler.Handle(new GetBorrowedBooksByUserRequest() { UserId = 1 }, new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(2);
    }
    [Fact]
    public async Task GetBorrowedBooksByUserHandlerShouldCallRepository()
    {
        //Arrange
        var fakeRepository = new Mock<IBorrowRepository>();
        fakeRepository.Setup(x => x.GetBorrowedBooksByUserIdAsync(It.IsAny<int>())).Returns<int>(a => _borrowRepository.GetBorrowedBooksByUserIdAsync(a));
        var getBorrowedBooksByUserHandler = new GetBorrowedBooksByUserHandler(fakeRepository.Object);
        //Act
        var result = await getBorrowedBooksByUserHandler.Handle(new GetBorrowedBooksByUserRequest() { UserId = 1 }, new CancellationToken(false));
        //Assert
        fakeRepository.Verify(x => x.GetBorrowedBooksByUserIdAsync(1), Times.Once);
    }

    [Fact]
    public async Task GetBooksWithBorrowedDaysHandlerTest()
    {
        //Arrange
        var getBooksWithBorrowedDaysHandler = new GetBooksWithBorrowedDaysHandler(_borrowRepository);
        //Act
        var result = await getBooksWithBorrowedDaysHandler.Handle(new GetBooksWithBorrowedDaysRequest() { BookId = 220 }, new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.FirstOrDefault(x => x.BookId == 220).ShouldNotBeNull();
        ((int)result.FirstOrDefault(x => x.BookId == 220)!.BorrowedDaysCount).ShouldBe(5);
    }
    [Fact]
    public async Task GetBooksWithBorrowedDaysHandlerShouldCallRepository()
    {
        //Arrange
        var fakeRepository = new Mock<IBorrowRepository>();
        fakeRepository.Setup(x => x.GetAllReturnedBooksByBookIdAsync(It.IsAny<int>())).Returns<int>(a => _borrowRepository.GetAllReturnedBooksByBookIdAsync(a));
        var getBorrowedBooksByUserHandler = new GetBooksWithBorrowedDaysHandler(fakeRepository.Object);
        //Act
        var result = await getBorrowedBooksByUserHandler.Handle(new GetBooksWithBorrowedDaysRequest() { BookId = 220 }, new CancellationToken(false));
        //Assert
        fakeRepository.Verify(x => x.GetAllReturnedBooksByBookIdAsync(220), Times.Once);
    }

    [Fact]
    public async Task GetMostBorrowersHandlerTest()
    {
        //Arrange
        var getBooksWithBorrowedDaysHandler = new GetMostBorrowersHandler(_borrowRepository);
        //Act
        var result = await getBooksWithBorrowedDaysHandler.Handle(new GetMostBorrowersRequest() { StartDate = DateTime.UtcNow.AddDays(-11), EndDate = DateTime.Now.AddDays(-2) }, new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.FirstOrDefault(x => x.UserId == 1).ShouldNotBeNull();
    }
    [Fact]
    public async Task GetMostBorrowersHandlerShouldReturnNullWithWrongSpecifiedDate()
    {
        //Arrange
        var getBooksWithBorrowedDaysHandler = new GetMostBorrowersHandler(_borrowRepository);
        //Act
        var result = await getBooksWithBorrowedDaysHandler.Handle(new GetMostBorrowersRequest() { StartDate = DateTime.UtcNow.AddDays(-3), EndDate = DateTime.Now.AddDays(-2) }, new CancellationToken(false));
        //Assert
        result.ShouldNotBeNull();
        result.FirstOrDefault(x => x.UserId == 1).ShouldBeNull();
    }
}