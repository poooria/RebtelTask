using System.Linq;
using Shouldly;
using Xunit;

namespace StarterFunctions.Tests
{
    public class StarterFunctionsShould
    {
        [Fact]
        public void Is2PowerTest()
        {
            //Arrange
            var functions = new Functions();
            //Act
            var power2 = functions.Is2Power(16);
            var notPower2 = functions.Is2Power(15);
            //Assert
            power2.ShouldBe(true);
            notPower2.ShouldBe(false);
        }
        [Fact]
        public void ReverseTest()
        {
            //Arrange
            var functions = new Functions();
            //Act
            var result = functions.Reverse("poorya");
            //Assert
            result.ShouldBe("ayroop");
        }
        [Fact]
        public void ReplicateTest()
        {
            //Arrange
            var functions = new Functions();
            //Act
            var result = functions.Replicate("poorya", 2);
            //Assert
            result.ShouldBe("pooryapoorya");
        }
        [Fact]
        public void OddNumbersTest()
        {
            //Arrange
            var functions = new Functions();
            //Act
            var result = functions.PrintOddNumbers();
            //Assert
            result.Count().ShouldBe(50);
            result.First().ShouldBe(1);
            result.Last().ShouldBe(99);
            result.Any(x => x % 2==0).ShouldBe(false);
        }
    }
}