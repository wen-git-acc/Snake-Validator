using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Payload;

public class State
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
    public Fruit Fruit { get; set; }

    [JsonPropertyName("snake")]
    public Snake Snake { get; set; }
}