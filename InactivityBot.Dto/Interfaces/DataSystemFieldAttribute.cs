namespace InactivityBot.Dto
{
    public class DataSystemFieldAttribute : ConfigTypeAttribute<bool>
    {
        public DataSystemFieldAttribute(bool value = true) : base(value)
        {
        }
    }
}