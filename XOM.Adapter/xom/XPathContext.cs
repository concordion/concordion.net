namespace nu.xom
{
    public class XPathContext
    {
        public string Prefix { get; set; }

        public string Uri { get; set; }

        public XPathContext(string prefix, string uri)
        {
            this.Prefix = prefix;
            this.Uri = uri;
        }
    }
}
