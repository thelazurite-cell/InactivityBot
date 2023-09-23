namespace InactivityBot.Dto
{
    public class CollectionFilterAttribute : ConfigTypeAttribute<bool>
    {
        public CollectionFilterAttribute(bool value = true) : base(value) { }
    }
}