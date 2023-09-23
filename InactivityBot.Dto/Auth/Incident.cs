using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using InactivityBot.Dto.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace InactivityBot.Dto.Auth
{
    public class Incident : DataTransferObject
    {
        [BsonElement("userId")]
        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [BsonElement("remoteAddress")]
        [JsonPropertyName("remoteAddress")]
        public string RemoteAddress { get; set; }

        [BsonElement("incidentType")]
        [JsonPropertyName("incidentType")]
        public IncidentType IncidentType { get; set; }

        [BsonElement("incidentLevel")]
        [JsonPropertyName("incidentLevel")]
        public IncidentLevel IncidentLevel { get; set; }

        [BsonElement("parameters")]
        [JsonPropertyName("parameters")]
        public List<string> Parameters { get; set; }

        [BsonElement("timestamp")]
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

    }
}