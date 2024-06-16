using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Response
{
    public class FruitConfig
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
