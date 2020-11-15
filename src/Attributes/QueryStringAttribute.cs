using System;

namespace RefaceCore.HttpProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class QueryStringAttribute : Attribute
    {
        public string Name { get; private set; }

        public QueryStringAttribute(string name)
        {
            Name = name;
        }
    }
}
