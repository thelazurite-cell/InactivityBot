namespace InactivityBot.Dto
{
    public class CollectionCloneAttribute : ConfigTypeAttribute<bool>
    {
        public CollectionCloneAttribute(bool value = true) : base(value) { }
    }
}