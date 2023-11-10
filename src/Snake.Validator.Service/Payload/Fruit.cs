using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Payload
{
    public class Fruit
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
