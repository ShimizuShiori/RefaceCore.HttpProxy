namespace RefaceCore.HttpProxy.Components
{
    /// <summary>
    /// Url 构建器
    /// </summary>
    public interface IUrlBuilder
    {
        /// <summary>
        /// 设置模板
        /// </summary>
        /// <param name="template"></param>
        void SetTemplate(string template);

        /// <summary>
        /// 设计路径值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void SetPathVariable(string name, string value);

        /// <summary>
        /// 添加 QueryString
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void AddQueryString(string name, string value);

        /// <summary>
        /// 生成 Url
        /// </summary>
        /// <returns></returns>
        string Build();
    }
}
