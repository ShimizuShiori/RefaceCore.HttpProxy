using System;

namespace RefaceCore.HttpProxy.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class HttpVerbAttribute : Attribute, IHttpVerb
    {
        public Verbs Verb { get; private set; }
        public HttpVerbAttribute(Verbs verb)
        {
            Verb = verb;
        }
    }
}
