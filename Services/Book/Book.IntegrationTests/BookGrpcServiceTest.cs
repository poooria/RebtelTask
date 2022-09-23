using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Book.Contracts;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace Book.IntegrationTests
{
    public class BookGrpcServiceTest : IClassFixture<MyApplicationFactory>
    {
        private readonly GrpcChannel _channel;
        private readonly IGrpcBookService _client;
        public BookGrpcServiceTest(MyApplicationFactory factory, ITestOutputHelper output)
        {
            var client = factory.CreateDefaultClient();
            _channel = GrpcChannel.ForAddress(client.BaseAddress, new GrpcChannelOptions
            {
                HttpClient = client
            });
            _client = _channel.CreateGrpcService<IGrpcBookService>();

        }
        [Fact]
        public async void GetBookAvailableBorrow()
        {
            //Arrange
            var request = new GetBookAvailableBorrowRequest() { BookId = 7 };
            //Act
            var result = await _client.GetBookAvailableBorrowAsync(request, CallContext.Default);
            //Assert
            result.Available.ShouldBe(27);
            result.Borrowed.ShouldBe(3);

        }
        [Fact]
        public async void GetBooksByIds()
        {
            //Arrange
            var request = new GetBooksByIdsRequest() { Ids = "1,2" };
            //Act
            var result = await _client.GetBooksByIdsAsync(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(2);
        }
        [Fact]
        public async Task GetBooksByIdsWithWrongFormatThrowException()
        {
            //Arrange
            var request = new GetBooksByIdsRequest() { Ids = "d" };
            //Act
            var ex = await Assert.ThrowsAsync<RpcException>(() => _client.GetBooksByIdsAsync(request, CallContext.Default));
            //Assert
            ex.Message.ShouldContain("comma");
        }
    }

}