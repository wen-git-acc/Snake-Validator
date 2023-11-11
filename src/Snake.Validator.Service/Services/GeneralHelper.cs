using Snake.Validator.Service.Response;

namespace Snake.Validator.Service.Services;

public class GeneralHelper : IGeneralHelper
{
    public FruitConfig GenerateFruitPosition(int width, int height, Random? random=null)
    {
        random ??= new Random();
        var fruitPositionX = 0;
        var fruitPositionY = 0;
        var isStartingPosition = true;

        if (width == 0 || height == 0)
        {
            throw new ArgumentException("Width and height given should not be zero");
        }

        while (isStartingPosition)
        {
            fruitPositionX = random.Next(width + 1);
            fruitPositionY = random.Next(height + 1);
            isStartingPosition = (fruitPositionX == 0 && fruitPositionY == 0);
        }

        return new FruitConfig()
        {
            X = fruitPositionX,
            Y = fruitPositionY,
        };
    }
}