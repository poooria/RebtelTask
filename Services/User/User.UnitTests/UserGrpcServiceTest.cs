using System.Collections.Generic;
using Moq;
using Shouldly;
using User.API.Grpc;
using User.API.Model;
using User.Contracts;

namespace User.UnitTests;

public class UserGrpcServiceTest
{
    [Fact]
    public async Task GetUsersByIdsTest()
    {
        //Arrange
        var fakeRepo = new Mock<IUserRepository>();
        fakeRepo.Setup(x => x.GetUsersByIdsAsync(It.IsAny<IEnumerable<int>>())).Returns(GetFakeUsers());

        //Action
        var service = new GrpcUserService(fakeRepo.Object);
        var result = await service.GetUsersByIds(new GetUsersByIdsRequest() { Ids = "1,2,3" });
        //Assert
        result.Count.ShouldBe(2);
    }

    private Task<List<UserModel>> GetFakeUsers()
    {
        return Task.FromResult(new List<UserModel>
        {
            new UserModel("Poorya","Ghajar","Poooria@gmail.com","+989188708991",new Address("Iran","Kurdistan","Sanandaj","Abidar","6668541231")),
            new UserModel("Kamyar","Babaei","Kamyar.b@gmail.com","+98918542263",new Address("Iran","Kurdistan","Sanandaj","Abidar","6668541231")),
           
        });
    }
}