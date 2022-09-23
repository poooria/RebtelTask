using Shouldly;
using User.API.Model;

namespace User.UnitTests
{
    public class UserRepositoryTests
    {
        private readonly UserRepository _userRepository;
        public UserRepositoryTests()
        {
            var dbOptions = InMemoryDatabaseOptionCreator.CreateNewContextOptions();
            var dbContext = new UserContext(dbOptions);
            _userRepository = new UserRepository(dbContext);
            dbContext.Users.AddRange(new UserModel("Poorya", "Ghajar", "Poooria@gmail.com", "+989188708991", new Address("Iran", "Kurdistan", "Sanandaj", "Abidar", "6668541231")),
                new UserModel("Kamyar", "Babaei", "Kamyar.b@gmail.com", "+98918542263", new Address("Iran", "Kurdistan", "Sanandaj", "Abidar", "6668541231"))
            );
            dbContext.SaveChanges();
        }
        [Fact]
        public async void GetUsersByIdsAsyncTest()
        {
            //Arrange in the constructor
            //Act
            var result = await _userRepository.GetUsersByIdsAsync(new[] { 1, 2, 3 });
            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            result.FirstOrDefault(x => x.Id == 3).ShouldBeNull();
            result.FirstOrDefault(x => x.FirstName == "Poorya").ShouldNotBeNull();

        }
    }
}