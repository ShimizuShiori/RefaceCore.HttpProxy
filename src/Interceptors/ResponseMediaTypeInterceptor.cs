using RefaceCore.HttpProxy.Attributes;
using System.Net.Http;
using System.Reflection;

namespace RefaceCore.HttpProxy.Interceptors
{
    /// <summary>
    /// 覆盖原本的 MediaType
    /// </summary>
    public class ResponseMediaTypeInterceptor : Interceptor
    {
        public override void OnRespond(IInterceptorContext context, HttpResponseMessage response)
        {
            ResponseMediaTypeAttribute attr = context.Method.GetCustomAttribute<ResponseMediaTypeAttribute>();
            if (attr == null)
                return;

            if (response.Content.Headers.ContentType == null)
                return;

            response.Content.Headers.ContentType.MediaType = attr.MediaType;
        }
    }
}
