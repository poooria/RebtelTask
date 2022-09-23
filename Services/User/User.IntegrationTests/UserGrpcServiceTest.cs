using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using Shouldly;
using User.Contracts;
using Xunit;
using Xunit.Abstractions;

namespace User.IntegrationTests
{
    public class UserGrpcServiceTest : IClassFixture<MyApplicationFactory>
    {
        private readonly GrpcChannel _channel;
        private readonly IGrpcUserService _client;
        public UserGrpcServiceTest(MyApplicationFactory factory, ITestOutputHelper output)
        {
            var client = factory.CreateDefaultClient();
            _channel = GrpcChannel.ForAddress(client.BaseAddress, new GrpcChannelOptions
            {
                HttpClient = client
            });
            _client = _channel.CreateGrpcService<IGrpcUserService>();

        }

        [Fact]
        public async void GetUsersByIdsTest()
        {
            //Arrange
            var request = new GetUsersByIdsRequest() { Ids = "1,2,3,4,5,6,7,8,9,10" };
            //Act
            var result = await _client.GetUsersByIds(request, CallContext.Default);
            //Assert
            result.Count.ShouldBe(7);
            result.FirstOrDefault(x => x.FirstName == "Poorya").ShouldNotBeNull();

        }

        [Fact]
        public async Task GetUsersByIdsWithWrongIdFormatShouldThrow()
        {
            //Arrange
            var request = new GetUsersByIdsRequest() { Ids = "d" };
            //Act
            var ex = await Assert.ThrowsAsync<RpcException>(() => _client.GetUsersByIds(request, CallContext.Default));
            //Assert
            ex.Message.ShouldContain("comma");
        }
    }

}