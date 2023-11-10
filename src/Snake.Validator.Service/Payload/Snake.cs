﻿using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Payload;

public class Snake
{
    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }

    [JsonPropertyName("velX")]
    public int VelX { get; set; }

    [JsonPropertyName("velY")]
    public int VelY { get; set; }
}