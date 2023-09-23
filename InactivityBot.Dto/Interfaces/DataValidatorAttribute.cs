namespace InactivityBot.Dto
{
    public class DataValidatorAttribute : ConfigTypeAttribute<string>
    {
        public DataValidatorAttribute(string value) : base(value)
        {
        }
    }
}