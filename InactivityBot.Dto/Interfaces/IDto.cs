using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InactivityBot.Dto.Interfaces
{
    public interface IDto
    {
        [BsonId()]
        [JsonIgnore]
        ObjectId Id { get; set; }

        [BsonIgnore]
        [JsonPropertyName("id")]
        string Identifier
        {
            get => Id.ToString();
        }

        [BsonIgnore]
        [JsonExtensionData]
        [JsonPropertyName("additionalData")]
        [DataSystemField]
        Dictionary<string, object> AdditionalData { get; set; }
    }
}