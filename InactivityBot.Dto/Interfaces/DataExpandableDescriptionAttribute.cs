namespace InactivityBot.Dto
{
    public class DataExpandableDescriptionAttribute : ConfigTypeAttribute<bool>
    {
        public DataExpandableDescriptionAttribute(bool value = true) : base(value)
        {
        }
    }
}