using System.Collections.Generic;

namespace RefaceCore.HttpProxy
{
    /// <summary>
    /// 配置
    /// </summary>
    public class HttpProxyOptions
    {
        /// <summary>
        /// Fallback 在等待60秒后会进行重试
        /// </summary>
        public int FallbackRetrySeconds { get; set; } = 60;

        /// <summary>
        /// ClientName 与 Host 的映射关系
        /// </summary>
        public IDictionary<string, string> ClientNameToHostMap { get; private set; }

        public HttpProxyOptions(IDictionary<string, string> clientNameToHostMap)
        {
            ClientNameToHostMap = clientNameToHostMap;
        }
    }
}
