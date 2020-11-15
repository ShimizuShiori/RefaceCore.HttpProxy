using RefaceCore.HttpProxy;
using RefaceCore.HttpProxy.Attributes;
using RefaceCore.HttpProxy.Interceptors;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 构建器
    /// </summary>
    public class HttpProxyBuilder
    {
        private readonly IServiceCollection services;

        public IDictionary<string, string> ClientNameToHostMap { get; private set; }

        public HttpProxyBuilder(IServiceCollection services)
        {
            this.services = services;
            this.ClientNameToHostMap = new Dictionary<string, string>();
        }

        /// <summary>
        /// 添加一个 Host 信息
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        public HttpProxyBuilder AddHost(string clientName, string host)
        {
            this.ClientNameToHostMap[clientName] = host;
            return this;
        }

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public HttpProxyBuilder AddClientType(Type type)
        {
            services.AddTransient(type, sp =>
            {
                IHttpProxyFactory factory = sp.GetService<IHttpProxyFactory>();
                MethodInfo method = factory.GetType().GetMethod("Create").MakeGenericMethod(type);
                return method.Invoke(factory, null);
            });
            MockAttribute mockAttr = type.GetCustomAttribute<MockAttribute>();
            if (mockAttr != null)
            {
                services.AddSingleton(mockAttr.Type);
            }
            return this;
        }

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public HttpProxyBuilder AddClientType<T>()
        {
            return this.AddClientType(typeof(T));
        }

        /// <summary>
        /// 添加更多的内容序列化工具
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public HttpProxyBuilder AddContentSerializers<T>() where T : IContentSerializer
        {
            return this.AddContentSerializers(typeof(T));
        }

        /// <summary>
        /// 添加更多的内容序列化工具
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public HttpProxyBuilder AddContentSerializers(Type type)
        {
            this.services.AddSingleton(typeof(IContentSerializer), type);
            return this;
        }

        /// <summary>
        /// 添加一个拦截器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public HttpProxyBuilder AddInterceptor(Type type)
        {
            this.services.AddSingleton(typeof(IInterceptor), type);
            return this;
        }

        public HttpProxyBuilder AddInterceptor<T>() where T : IInterceptor
        {
            return this.AddInterceptor(typeof(T));
        }
    }
}
