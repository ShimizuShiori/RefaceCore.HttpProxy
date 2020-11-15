using System;

namespace RefaceCore.HttpProxy.Attributes
{
    /// <summary>
    /// 表示该参数放置在请求的主体中
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ContentAttribute : Attribute
    {
        /// <summary>
        /// application/x-www-form-urlencoded
        /// </summary>
        public const string X_WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";

        /// <summary>
        /// application/json
        /// </summary>
        public const string JSON = "application/json";

        public string MediaType { get; private set; }

        public ContentAttribute(string mediaType)
        {
            MediaType = mediaType;
        }
    }
}
