using System;

namespace RefaceCore.HttpProxy
{
    public interface IHttpProxyFactory
    {
        T Create<T>();
    }
}
