using System;

namespace RefaceCore.HttpProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ClientNameAttribute : Attribute
    {
        public string Name { get; private set; }

        public ClientNameAttribute(string name)
        {
            Name = name;
        }
    }
}
