using RefaceCore.HttpProxy;
using RefaceCore.HttpProxy.Components;
using RefaceCore.HttpProxy.ContentSerializers;
using RefaceCore.HttpProxy.Interceptors;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extension
    {
        /// <summary>
        /// 配置 Http 代理
        /// </summary>
        /// <param name="services"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigHttpProxy(this IServiceCollection services, Action<HttpProxyBuilder> action)
        {
            services.AddSingleton<IHttpProxyFactory, DefaultHttpProxyFactory>();
            services.AddSingleton<IContentSerializer, XWwwFormUrlencoded>();
            services.AddSingleton<IContentSerializer, Json>();
            services.AddSingleton<IUrlBuilder, DefaultUrlBuilder>();

            HttpProxyBuilder builder = new HttpProxyBuilder(services);
            action(builder);
            builder.AddInterceptor<MockInterceptor>();
            builder.AddInterceptor<ResponseMediaTypeInterceptor>();

            HttpProxyOptions options = new HttpProxyOptions(builder.ClientNameToHostMap);
            services.AddSingleton(options);

            return services;
        }
    }
}
