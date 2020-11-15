using System.Net.Http;

namespace RefaceCore.HttpProxy.Interceptors
{
    /// <summary>
    /// 默认的拦截器，没有任何方法，只是抽象类和虚方法
    /// </summary>
    public abstract class Interceptor : IInterceptor
    {
        public virtual void OnClientUrlFound(IInterceptorContext context, ref string url)
        {
        }

        public virtual void OnHttpClientPrepaired(IInterceptorContext context)
        {
        }

        public virtual void OnPreInvoke(IInterceptorContext context)
        {
        }

        public virtual void OnRespond(IInterceptorContext context, HttpResponseMessage response)
        {
        }
    }
}
