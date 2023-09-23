using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using InactivityBot.Dto.Converters;
using InactivityBot.Dto.Filtering;

namespace InactivityBot.Dto.Interfaces
{
    public interface IComparableItem: ITypeDiscriminator
    {
        
        [JsonPropertyName("comparisonType")]
        public FilterType? ComparisonType { get; set; }
        
        [JsonPropertyName("typeDiscriminator")]
        public string TypeDiscriminator { get; }
    }
}