using System;
using System.Linq;

namespace RefaceCore.HttpProxy.ContentSerializers
{
    public class XWwwFormUrlencoded : IContentSerializer
    {
        public object Deserialize(string text, Type type)
        {
            throw new NotImplementedException("application/x-www-form-urlencoded 不支持反序列化");
        }

        public bool MatchMediaType(string mediaType)
        {
            return mediaType == "application/x-www-form-urlencoded";
        }

        public string Serialize(object obj)
        {
            var items = obj.GetType().GetProperties()
                .Select(x => $"{x.Name}={x.GetValue(obj)}");
            return string.Join("&", items);
        }
    }
}
