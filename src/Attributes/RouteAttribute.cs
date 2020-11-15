using System;

namespace RefaceCore.HttpProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false)]
    public class RouteAttribute : Attribute
    {
        public string Path { get; }

        public RouteAttribute(string path = "")
        {
            Path = path;
        }
    }
}
