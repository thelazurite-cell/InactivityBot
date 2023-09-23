namespace InactivityBot.Dto
{
    public class DataRelationshipViewAttribute : ConfigTypeAttribute<bool>
    {
        public DataRelationshipViewAttribute(bool value = true) : base(value)
        {
        }
    }
}