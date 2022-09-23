using System;
using System.Threading;
using Library.Aggregator.Controllers;
using Library.Aggregator.DTO.Requests;
using Library.Aggregator.Services;
using MediatR;
using Moq;
using Xunit;
using AuthenticateRequest = Library.Aggregator.DTO.Requests.AuthenticateRequest;

namespace Library.Aggregator.UnitTests
{
    public class ApiTests
    {
        [Fact]
        public async void AuthenticateShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.Authenticate(new AuthenticateRequest() { UserName = "Poorya", Password = "dkjsdksdj" });
            //Assert
            mediator.Verify(x => x.Send(It.Is<AuthenticateRequest>(y => y.UserName == "Poorya"), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetMostBorrowedBooksShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.GetMostBorrowedBooks();
            //Assert
            mediator.Verify(x => x.Send(It.Is<MostBorrowedBooksRequest>(y => y != null), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetBookAvailableShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.GetBookAvailable(1);
            //Assert
            mediator.Verify(x => x.Send(It.Is<BookAvailableBorrowRequest>(y => y.BookId == 1), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetMostBorrowersShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.GetMostBorrowers(new MostBorrowersRequest()
            { StartDate = new DateTime(2022, 9, 22), EndDate = DateTime.Now });
            //Assert
            mediator.Verify(x => x.Send(It.Is<MostBorrowersRequest>(y => y.StartDate.Year == 2022), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetOtherBooksAlsoBorrowedShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.GetOtherBooksAlsoBorrowed(1);
            //Assert
            mediator.Verify(x => x.Send(It.Is<OtherBooksAlsoBorrowedRequest>(y => y.BookId == 1), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetBookReadRateShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.GetBookReadRate(1);
            //Assert
            mediator.Verify(x => x.Send(It.Is<BookReadRateRequest>(y => y.BookId == 1), It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async void GetUserBorrowedBooksShouldSendRequestToTheHandler()
        {
            //Arrange
            var mediator = new Mock<IMediator>();
            var api = new LibraryController(mediator.Object);
            //Act
            var result = await api.GetUserBorrowedBooks(1);
            //Assert
            mediator.Verify(x => x.Send(It.Is<UserBorrowedBooksRequest>(y => y.UserId == 1), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}