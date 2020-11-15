using System;

namespace RefaceCore.HttpProxy.Interceptors
{
    public class OriginalMethod : IOriginalMethod
    {
        private Func<object[], object> invoker;

        public OriginalMethod(Func<object[], object> invoker)
        {
            this.invoker = invoker;
        }

        public object Execute(object[] args)
        {
            return invoker(args);
        }
    }
}
