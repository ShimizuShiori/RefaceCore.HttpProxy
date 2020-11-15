using RefaceCore.HttpProxy.Attributes;
using System.Linq;
using System.Reflection;

namespace RefaceCore.HttpProxy
{
    public class ClientMethodDescription
    {
        public Verbs Verb { get; private set; }
        public RouteAttribute TypeRoute { get; private set; }
        public RouteAttribute MethodRoute { get; private set; }
        public ClientMethodParameterDescription[] ParameterDescriptions { get; private set; }
        public ClientMethodDescription(MethodInfo method)
        {
            this.Verb = (method.GetCustomAttribute<HttpVerbAttribute>() ?? new HttpVerbAttribute(Verbs.Get)).Verb;
            this.TypeRoute = method.DeclaringType.GetCustomAttribute<RouteAttribute>() ?? new RouteAttribute();
            this.MethodRoute = method.GetCustomAttribute<RouteAttribute>() ?? new RouteAttribute();
            this.ParameterDescriptions = method.GetParameters().Select(x => new ClientMethodParameterDescription(x)).ToArray();
        }
    }
}
