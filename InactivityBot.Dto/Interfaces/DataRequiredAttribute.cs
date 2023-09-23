namespace InactivityBot.Dto
{
    public class DataRequiredAttribute : ConfigTypeAttribute<bool>
    {
        public DataRequiredAttribute(bool value = true) : base(value)
        {
        }
    }
}