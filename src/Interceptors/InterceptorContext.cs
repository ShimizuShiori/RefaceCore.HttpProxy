using System;
using System.Net.Http;
using System.Reflection;

namespace RefaceCore.HttpProxy.Interceptors
{
    public class InterceptorContext : IInterceptorContext
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public HttpClient HttpClient { get; private set; }

        public MethodInfo Method { get; private set; }

        public object[] Arguments { get; private set; }
        public object Result { get; set; }

        public IOriginalMethod OriginalMethod { get; private set; }

        public InterceptorContext(IServiceProvider serviceProvider, HttpClient httpClient, MethodInfo method, object[] arguments, IOriginalMethod originalMethod)
        {
            ServiceProvider = serviceProvider;
            HttpClient = httpClient;
            Method = method;
            Arguments = arguments;
            OriginalMethod = originalMethod;
        }
    }
}
