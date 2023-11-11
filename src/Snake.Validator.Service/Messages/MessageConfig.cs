namespace Snake.Validator.Service.Messages
{
    public class MessageConfig
    {
        public static string GameOverPosition = "Game is over, snake went out of bounds.";
        public static string GameOverDirection = "Game is over, snake made an invalid move.";
        public static string FruitNotFound = "Fruit not found, the ticks do not lead the snake to the fruit position.";
        public static string IncorrectStartingSize = "Given Width and Height must be more than 5";
        public static string GenerateFruitPosException = "Width and height given should not be zero";
    }
}
