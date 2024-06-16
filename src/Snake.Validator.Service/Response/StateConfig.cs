using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Response
{
    public class StateConfig
    {
        [JsonPropertyName("gameId")]
        public string GameID { get; set; }

        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("fruit")]
        public FruitConfig Fruit { get; set; }

        [JsonPropertyName("snake")]
        public SnakeConfig Snake { get; set; }
    }
}