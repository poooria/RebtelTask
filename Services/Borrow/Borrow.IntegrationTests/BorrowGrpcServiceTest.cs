using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Borrow.API.Grpc;
using Borrow.Contracts.DTO;
using Borrow.Contracts.Services;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Borrow.IntegrationTests
{
    public class BorrowGrpcServiceTest : IClassFixture<MyApplicationFactory>
    {
        private readonly GrpcChannel _channel;
        private readonly IGrpcBorrowService _client;
        public BorrowGrpcServiceTest(MyApplicationFactory factory, ITestOutputHelper output)
        {
            var client = factory.CreateDefaultClient(new ResponseVersionHandler());
            _channel = GrpcChannel.ForAddress(client.BaseAddress, new GrpcChannelOptions
            {
                HttpClient = client
            });
            _client = _channel.CreateGrpcService<IGrpcBorrowService>();

        }
        private class ResponseVersionHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                var response = await base.SendAsync(request, cancellationToken);
                response.Version = request.Version;
                return response;
            }
        }
        [Fact]
        public async void GetMostBorrowedBooksAsyncTest()
        {
            //Arrange
            var request = new GetMostBorrowedBooksRequest();
            //Act
            var result = await _client.GetMostBorrowedBooksAsync(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(10);
        }
        [Fact]
        public async void BorrowedBooksByUserIdTest()
        {
            //Arrange
            var request = new GetBorrowedBooksByUserRequest() { UserId = 1 };
            //Act
            var result = await _client.GetBorrowedBooksByUserIdAsync(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(8);
        }
        [Fact]
        public async void GetBookWithBorrowedDaysTest()
        {
            //Arrange
            var request = new GetBooksWithBorrowedDaysRequest() { BookId = 1 };
            //Act
            var result = await _client.GetBookWithBorrowedDaysAsync(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(5);
        }
        [Fact]
        public async void GetMostBorrowersAsyncTest()
        {
            //Arrange
            var request = new GetMostBorrowersRequest() { StartDate = new DateTime(2022, 01, 01), EndDate = new DateTime(2022, 12, 01) };
            //Act
            var result = await _client.GetMostBorrowersAsync(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(7);
            result.FirstOrDefault(x => x.UserId == 1).ShouldNotBeNull();
            result.FirstOrDefault(x => x.UserId == 1).BorrowedCount.ShouldBe(8);
        }
        [Fact]
        public async void GetOtherBooksAlsoBorrowedTest()
        {
            //Arrange
            var request = new GetOtherBooksBorrowedRequest() { BookId = 1 };
            //Act
            var result = await _client.GetOtherBooksAlsoBorrowedAsync(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(9);
            result.FirstOrDefault(x => x.BookId == 6).ShouldNotBeNull();
            result.FirstOrDefault(x => x.BookId == 6).BorrowedCount.ShouldBe(5);
        }
    }
}