using System;

namespace InactivityBot.Dto
{
    public class ConfigTypeAttribute<T> : Attribute
    {
        public T Value { get; }

        public ConfigTypeAttribute(T value)
        {
            Value = value;
        }
    }
}