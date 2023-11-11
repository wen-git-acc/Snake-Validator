using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Payload
{
    public class VelocityTicks
    {
        [JsonPropertyName("velX")]
        public int VelX { get; set; }

        [JsonPropertyName("velY")]
        public int VelY { get; set; }
    }
}

