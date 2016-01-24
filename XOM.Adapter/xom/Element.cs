using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace nu.xom
{
    public class Element : ParentNode
    {
        public XElement XElement { get; private set; }

        public Element(string name) 
            : this(new XElement(name))
        { }

        public Element(XElement xNode)
            : base(xNode)
        {
            this.XElement = xNode;
        }

        public string getValue()
        {
            return this.XElement.Value;
        }

        public void appendChild(string text)
        {
            this.XElement.Add(text);
        }

        public void appendChild(Text text)
        {
            var xText = text.XText;
            this.XElement.Add(xText);
        }

        public void removeChild(Node child)
        {
            child.detach();
        }

        public void addAttribute(Attribute attribute)
        {
            var xName = attribute.XAttribute.Name;
            var value = attribute.XAttribute.Value;
            this.XElement.SetAttributeValue(xName, value);
        }

        public string getAttributeValue(string name)
        {
            var xName = XName.Get(name.Substring(name.LastIndexOf(":") + 1));
            XAttribute attribute = this.XElement.Attribute(xName);
            return (attribute != null) ? attribute.Value : null;
        }

        public string getAttributeValue(string localName, string namespaceURI)
        {
            XAttribute attribute = this.XElement.Attribute(XName.Get(localName, namespaceURI));
            return (attribute != null) ? attribute.Value : null;
        }

        public Attribute getAttribute(int index)
        {
            var xAttribute = this.XElement.Attributes().ElementAt(index);
            return new Attribute(xAttribute);
        }

        public Attribute getAttribute(string name)
        {
            return new Attribute(this.XElement.Attributes(XName.Get(name)).ElementAt(0));
        }

        public int getAttributeCount()
        {
            return this.XElement.Attributes().Count();
        }

        public Attribute removeAttribute(Attribute attribute)
        {
            attribute.detach();
            return attribute;
        }

        public Element getFirstChildElement(string name)
        {
            foreach (XElement descendant in this.XElement.Descendants())
            {
                if (descendant.Name.LocalName == name)
                {
                    return new Element(descendant);
                }
            }
            return null;
        }

        public string getLocalName()
        {
            return this.XElement.Name.LocalName;
        }

        public Elements getChildElements()
        {
            return new Elements(this.XElement.Elements());
        }

        public Elements getChildElements(string name)
        {
            List<XElement> result = new List<XElement>();
            var allChildElements = this.XElement.Elements();
            foreach (var childElement in allChildElements)
            {
                if (childElement.Name.LocalName.Equals(name) || 
                    childElement.Name.LocalName.Length == 0)
                {
                    result.Add(childElement);
                }
            }
            return new Elements(result);
        }
    }
}
