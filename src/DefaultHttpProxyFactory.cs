using System;
using System.Net.Http;
using System.Reflection;

namespace RefaceCore.HttpProxy
{
    public class DefaultHttpProxyFactory : IHttpProxyFactory
    {
        private readonly IServiceProvider serviceProvider;

        public DefaultHttpProxyFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public T Create<T>()
        {
            var result = DispatchProxy.Create<T, HttpClientDispatchProxy>();
            HttpClientDispatchProxy proxy = result as HttpClientDispatchProxy;
            proxy.HttpClient = new HttpClient();
            proxy.ServiceProvider = serviceProvider;
            return result;
        }
    }
}
