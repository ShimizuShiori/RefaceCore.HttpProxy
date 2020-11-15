using RefaceCore.HttpProxy.Attributes;
using System.Reflection;

namespace RefaceCore.HttpProxy
{
    public class ClientMethodParameterDescription
    {
        public QueryStringAttribute QueryStringAttribute { get; private set; }
        public PathVariableAttribute PathVariableAttribute { get; private set; }
        public ContentAttribute ContentAttribute { get; private set; }

        public ParameterInfo ParameterInfo { get; private set; }

        public ClientMethodParameterDescription(ParameterInfo parameterInfo)
        {
            this.ParameterInfo = parameterInfo;
            QueryStringAttribute = parameterInfo.GetCustomAttribute<QueryStringAttribute>();
            PathVariableAttribute = parameterInfo.GetCustomAttribute<PathVariableAttribute>(); ;
            ContentAttribute = parameterInfo.GetCustomAttribute<ContentAttribute>();
        }
    }
}
