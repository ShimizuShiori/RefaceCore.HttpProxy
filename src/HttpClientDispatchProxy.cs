using Microsoft.Extensions.DependencyInjection;
using RefaceCore.HttpProxy.Attributes;
using RefaceCore.HttpProxy.Components;
using RefaceCore.HttpProxy.Interceptors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace RefaceCore.HttpProxy
{
    /// <summary>
    /// 切面处理类
    /// </summary>
    public class HttpClientDispatchProxy : DispatchProxy
    {

        internal HttpClient HttpClient { get; set; }
        internal IServiceProvider ServiceProvider { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            IInterceptorContext context = new InterceptorContext(this.ServiceProvider,
                this.HttpClient,
                targetMethod,
                args,
                new OriginalMethod(_args => this.Invoke(targetMethod, _args))
            );

            IEnumerable<IInterceptor> interceptors = this.ServiceProvider.GetServices<IInterceptor>();
            foreach (var item in interceptors)
            {
                item.OnPreInvoke(context);
                if (context.Result != null)
                    return context.Result;
            }

            HttpProxyOptions option = this.ServiceProvider.GetService<HttpProxyOptions>();
            IEnumerable<IContentSerializer> contentSerializers = this.ServiceProvider.GetServices<IContentSerializer>();
            IUrlBuilder urlBuilder = this.ServiceProvider.GetService<IUrlBuilder>();

            ClientMethodDescription description = new ClientMethodDescription(targetMethod);


            string clientName = this.GetClientName(targetMethod);

            string url = option.ClientNameToHostMap[clientName];
            foreach (var item in interceptors)
            {
                item.OnClientUrlFound(context, ref url);
                if (context.Result != null) return context.Result;
            }

            if (description.TypeRoute.Path != "")
                url += description.TypeRoute.Path;
            url += description.MethodRoute.Path;
            urlBuilder.SetTemplate(url);

            HttpContent content = null;
            for (int i = 0; i < description.ParameterDescriptions.Length; i++)
            {
                var parameterDescr = description.ParameterDescriptions[i];

                if (parameterDescr.PathVariableAttribute != null)
                {
                    urlBuilder.SetPathVariable(parameterDescr.ParameterInfo.Name, args[i].ToString());
                    continue;
                }

                if (parameterDescr.QueryStringAttribute != null)
                {
                    urlBuilder.AddQueryString(parameterDescr.QueryStringAttribute.Name, args[i].ToString());
                    continue;
                }

                if (parameterDescr.ContentAttribute != null)
                {
                    foreach (var serializers in contentSerializers)
                    {
                        if (!serializers.MatchMediaType(parameterDescr.ContentAttribute.MediaType))
                            continue;
                        content = new StringContent(serializers.Serialize(args[i]));
                        content.Headers.ContentType = new MediaTypeHeaderValue(parameterDescr.ContentAttribute.MediaType);
                        break;
                    }
                }
            }
            foreach (var item in interceptors)
            {
                item.OnHttpClientPrepaired(context);
                if (context.Result != null) return context.Result;
            }
            HttpResponseMessage message;
            switch (description.Verb)
            {
                case Verbs.Get:
                    message = AsyncHelper.ExecuteAndWait(HttpClient.GetAsync(url));
                    break;
                case Verbs.Post:
                    message = AsyncHelper.ExecuteAndWait(HttpClient.PostAsync(url, content));
                    break;
                case Verbs.Put:
                    message = AsyncHelper.ExecuteAndWait(HttpClient.PutAsync(url, content));
                    break;
                case Verbs.Delete:
                    message = AsyncHelper.ExecuteAndWait(HttpClient.DeleteAsync(url));
                    break;
                case Verbs.Patch:
                    message = AsyncHelper.ExecuteAndWait(HttpClient.PatchAsync(url, content));
                    break;
                default:
                    throw new NotImplementedException();
            }

            foreach (var item in interceptors)
            {
                item.OnRespond(context, message);
                if (context.Result != null) return context.Result;
            }
            if (targetMethod.ReturnType == typeof(void))
                return null;

            if (targetMethod.ReturnType == typeof(HttpResponseMessage))
                return message;

            if (targetMethod.ReturnType == typeof(HttpContent))
                return message.Content;

            if (targetMethod.ReturnType == typeof(string))
                return AsyncHelper.ExecuteAndWait(message.Content.ReadAsStringAsync());

            if (targetMethod.ReturnType == typeof(Stream))
                return AsyncHelper.ExecuteAndWait(message.Content.ReadAsStreamAsync());

            if (targetMethod.ReturnType == typeof(byte[]))
                return AsyncHelper.ExecuteAndWait(message.Content.ReadAsByteArrayAsync());


            string mediaType = message.Content.Headers.ContentType.MediaType;
            foreach (var serializer in contentSerializers)
            {
                if (!serializer.MatchMediaType(mediaType))
                    continue;

                return serializer.Deserialize(AsyncHelper.ExecuteAndWait(message.Content.ReadAsStringAsync()), targetMethod.ReturnType);
            }

            return null;
        }

        private string GetClientName(MethodInfo method)
        {
            return (method.DeclaringType.GetCustomAttribute<ClientNameAttribute>() ?? new ClientNameAttribute(method.DeclaringType.Name)).Name;
        }
    }
}
