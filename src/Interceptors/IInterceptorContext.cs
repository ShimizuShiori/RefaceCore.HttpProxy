using System;
using System.Net.Http;
using System.Reflection;

namespace RefaceCore.HttpProxy.Interceptors
{
    /// <summary>
    /// 拦截器执行过程中的上下文对象
    /// </summary>
    public interface IInterceptorContext
    {
        /// <summary>
        /// IOC 容器
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 当前用于请求的 HttpClient 实例
        /// </summary>
        HttpClient HttpClient { get; }

        /// <summary>
        /// 正在拦截的方法
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// 正在拦截的参数
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        /// 直接生成结果，不再继续后续的流程
        /// </summary>
        object Result { get; set; }

        /// <summary>
        /// 原方法
        /// </summary>
        IOriginalMethod OriginalMethod { get; }
    }
}
