namespace RefaceCore.HttpProxy.Interceptors
{
    /// <summary>
    /// 原方法。
    /// 通过此组件，可以重新指定参数后，再次执行原函数
    /// </summary>
    public interface IOriginalMethod
    {
        /// <summary>
        /// 执行原本的方法，并返回执行后的结果
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        object Execute(object[] args);
    }
}
