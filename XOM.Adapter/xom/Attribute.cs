using System.Xml.Linq;

namespace nu.xom
{
    public class Attribute
    {
        public XAttribute XAttribute { private set; get; }

        public Attribute(string localName, string value)
            : this(new XAttribute(localName, value))
        { }

        public Attribute(XAttribute xAttribute)
        {
            this.XAttribute = xAttribute;
        }

        public string getNamespaceURI()
        {
            return this.XAttribute.Name.NamespaceName;
        }

        public string getLocalName()
        {
            return this.XAttribute.Name.LocalName;
        }

        public string getValue()
        {
            return this.XAttribute.Value;
        }

        public void setValue(string value)
        {
            this.XAttribute.SetValue(value);
        }

        public void detach()
        {
            this.XAttribute.Remove();
        }
    }
}
