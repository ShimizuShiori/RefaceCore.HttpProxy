using System;

namespace RefaceCore.HttpProxy
{
    public interface IContentSerializer
    {
        bool MatchMediaType(string mediaType);
        string Serialize(object obj);

        object Deserialize(string text, Type type);
    }
}
