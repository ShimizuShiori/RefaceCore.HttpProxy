using System;

namespace RefaceCore.HttpProxy.Attributes
{

    /// <summary>
    /// 重新指定响应的 MediaType
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ResponseMediaTypeAttribute : Attribute
    {
        public string MediaType { get; private set; }

        public ResponseMediaTypeAttribute(string mediaType)
        {
            MediaType = mediaType;
        }
    }
}
