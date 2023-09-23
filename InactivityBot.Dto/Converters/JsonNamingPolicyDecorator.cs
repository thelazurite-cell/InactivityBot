using System.Text.Json;

namespace InactivityBot.Dto.Converters
{
    public class JsonNamingPolicyDecorator : JsonNamingPolicy 
    {
        readonly JsonNamingPolicy underlyingNamingPolicy;
    
        public JsonNamingPolicyDecorator(JsonNamingPolicy underlyingNamingPolicy) => this.underlyingNamingPolicy = underlyingNamingPolicy;

        public override string ConvertName (string name) => underlyingNamingPolicy == null ? name : underlyingNamingPolicy.ConvertName(name);
    }
}