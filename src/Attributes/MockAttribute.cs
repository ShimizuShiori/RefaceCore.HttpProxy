using System;

namespace RefaceCore.HttpProxy.Attributes
{
    /// <summary>
    /// 通过给客户端添加此特征可以跳过真正的请求，而直接使用模拟数据返回
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class MockAttribute : Attribute
    {
        /// <summary>
        /// 使用哪个类型作为此接口的 Mock
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 使用哪个类型作为此接口的 Mock
        /// </summary>
        /// <param name="type"></param>
        public MockAttribute(Type type)
        {
            Type = type;
        }
    }
}
