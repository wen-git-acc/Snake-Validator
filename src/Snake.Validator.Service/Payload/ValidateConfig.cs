using Snake.Validator.Service.Response;
using System.Text.Json.Serialization;

namespace Snake.Validator.Service.Payload
{
    public class ValidateConfig : StateConfig
    {
        [JsonPropertyName("ticks")]
        public List<VelocityTicks> Ticks { get; set; }
    }
}
