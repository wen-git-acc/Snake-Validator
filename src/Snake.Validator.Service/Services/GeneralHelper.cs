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