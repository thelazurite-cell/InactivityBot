using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using InactivityBot.Dto.Interfaces;
using MongoDB.Bson.Serialization.Attributes;

namespace InactivityBot.Dto.Auth
{
    [TransferableDataType]
    [NoInsertFromApi]
    [NoUpdateFromApi]
    public class Token : DataTransferObject
    {
        [JsonPropertyName("token")]
        [BsonElement("token")]
        [DataSystemField]
        [DataType(DataTypeEnum.String)]
        public string DiscordToken { get; set; } = string.Empty;
    }

    public class NoInsertFromApiAttribute : Attribute
    {
    }

    public class NoUpdateFromApiAttribute : Attribute
    {
    }
}