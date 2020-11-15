using RefaceCore.HttpProxy.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace RefaceCore.HttpProxy.Interceptors
{
    public class MockInterceptor : Interceptor
    {
        public override void OnPreInvoke(IInterceptorContext context)
        {
            MockAttribute mockAttr = context.Method.DeclaringType.GetCustomAttribute<MockAttribute>();
            if (mockAttr == null)
                return;

            Object mockObject = context.ServiceProvider.GetService(mockAttr.Type);
            MethodInfo method = mockObject.GetType().GetMethod(context.Method.Name, context.Method.GetParameters().Select(x => x.ParameterType).ToArray());
            context.Result = method.Invoke(mockObject, context.Arguments);
        }
    }
}
