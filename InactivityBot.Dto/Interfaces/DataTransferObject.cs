using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace InactivityBot.Dto.Interfaces
{
    public abstract class DataTransferObject : IDto
    {
        [BsonId()]
        [JsonIgnore]
        [DataHidden]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        [JsonPropertyName("id")]
        [DataHidden]
        [DataSystemField]
        [DataType(DataTypeEnum.Id)]
        public string Identifier
        {
            get => Id.ToString();
        }

        [BsonIgnore]
        [JsonExtensionData]
        [JsonPropertyName("additionalData")]
        [DataSystemField]
        public Dictionary<string, object> AdditionalData { get; set; } = new();
    }
}