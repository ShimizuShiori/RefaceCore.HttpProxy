using System;
using System.Text.Json;

namespace RefaceCore.HttpProxy.ContentSerializers
{
    public class Json : IContentSerializer
    {
        public object Deserialize(string text, Type type)
        {
            return JsonSerializer.Deserialize(text, type);
        }

        public bool MatchMediaType(string mediaType)
        {
            return mediaType == "application/json";
        }

        public string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
    }
}
