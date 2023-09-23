namespace InactivityBot.Dto
{
    public class DataFriendlyNameAttribute : ConfigTypeAttribute<string>
    {
        public DataFriendlyNameAttribute(string value) : base(value)
        {
        }
    }
}