using FakeItEasy;
using Snake.Validator.Service.Response;
using Snake.Validator.Service.Services;
using Xunit;

namespace Snake.Validator.Service.Tests.Services;

public class GeneralHelperTests
{
    public IGeneralHelper GetService()
    {
        return new GeneralHelper();
    }


    [Theory]
    [InlineData(200,300,10)]
    public void GenerateFruitPositionTests_ReturnFruitConfigType(int width, int height, int expectedPosition)
    {
        //Arrange
        var service = GetService();
        var mockRandomizer = A.Fake<Random>();
        A.CallTo(() => mockRandomizer.Next(A<int>._)).Returns(expectedPosition);

        //Act
        var fruitPos = service.GenerateFruitPosition(width, height, mockRandomizer);

        //Assert
        Assert.IsType<FruitConfig>(fruitPos);
        Assert.Equal(expectedPosition, fruitPos.X);
        Assert.Equal(expectedPosition, fruitPos.Y);
        A.CallTo(() => mockRandomizer.Next(A<int>._))
            .MustHaveHappenedANumberOfTimesMatching(count => count == 2);
    }
}