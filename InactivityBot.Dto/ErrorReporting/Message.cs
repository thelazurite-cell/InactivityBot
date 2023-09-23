using System.Collections.Generic;
using System.Text.Json.Serialization;
using InactivityBot.Dto.Auth;

namespace InactivityBot.Dto
{
    public class Message
    {
        [JsonPropertyName("message")]
        public string MessageText { get; set; }
        [JsonPropertyName("errorCode")]
        public ApiErrorCode ErrorCode { get; set; }
        [JsonPropertyName("level")]
        public IncidentLevel Level { get; set; }
        [JsonPropertyName("parameters")]
        public List<string> Parameters { get; set; } = new List<string>();
    }
}