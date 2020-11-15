namespace RefaceCore.HttpProxy.Components
{
    public class DefaultUrlBuilder : IUrlBuilder
    {
        private string url;
        private bool hasQueryString = false;

        public void AddQueryString(string name, string value)
        {
            if (!hasQueryString)
            {
                url += "?";
                hasQueryString = true;
            }
            else
            {
                url += "&";
            }
            url += $"{name}={value}";
        }

        public string Build()
        {
            return this.url;
        }

        public void SetPathVariable(string name, string value)
        {
            url = url.Replace($"{{{name}}}", value);
        }

        public void SetTemplate(string template)
        {
            this.url = template;
        }
    }
}
