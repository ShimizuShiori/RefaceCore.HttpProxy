using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace RefaceCore.HttpProxy.Interceptors
{
    /// <summary>
    /// 拦截器，在 Http 代理类执行的过程中，对预设的位置进行干预
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// 执行前的拦截点
        /// </summary>
        /// <param name="context"></param>
        void OnPreInvoke(IInterceptorContext context);

        /// <summary>
        /// 当指定的客户端所对应的 Url 被发现后
        /// </summary>
        /// <param name="context"></param>
        /// <param name="url">Url 引用传值，可以在拦截器中替换</param>
        void OnClientUrlFound(IInterceptorContext context, ref string url);

        /// <summary>
        /// 当 Http 客户端准备完成后
        /// </summary>
        /// <param name="context"></param>
        void OnHttpClientPrepaired(IInterceptorContext context);

        /// <summary>
        /// 当请求发送完成并得到响应后
        /// </summary>
        /// <param name="context"></param>
        /// <param name="response"></param>
        void OnRespond(IInterceptorContext context, HttpResponseMessage response);

    }
}
