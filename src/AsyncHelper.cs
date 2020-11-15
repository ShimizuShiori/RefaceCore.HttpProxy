using System.Threading.Tasks;

namespace RefaceCore.HttpProxy
{
    public static class AsyncHelper
    {
        public static T ExecuteAndWait<T>(Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}
