using Snake.Validator.Service.Payload;

namespace Snake.Validator.Service.Services;

public class GeneralHelper : IGeneralHelper
{
    public Fruit GenerateFruitPosition(int width, int height)
    {
        var random = new Random();
        var fruitPositionX = 0;
        var fruitPositionY = 0;
        var isStartingPosition = true;

        while (isStartingPosition)
        {
            fruitPositionX = random.Next(width + 1);
            fruitPositionY = random.Next(height + 1);
            isStartingPosition = (fruitPositionX == 0 && fruitPositionY == 0);
        }

        return new Fruit()
        {
            X = fruitPositionX,
            Y = fruitPositionY,
        };
    }
}