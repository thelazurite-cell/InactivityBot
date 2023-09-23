using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using InactivityBot.Dto.Interfaces;

namespace InactivityBot.Dto.Filtering
{
    public class QueryGroup : IComparableItem
    {
        [JsonPropertyName("comparisonType")]
        public FilterType? ComparisonType { get; set; }

        [JsonPropertyName("typeDiscriminator")]
        public string TypeDiscriminator => nameof(QueryGroup);

        [JsonPropertyName("queries")]
        public List<IComparableItem> Queries { get; set; } = new();
    }
}