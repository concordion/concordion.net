using System.Collections.Generic;
using System.Xml.Linq;

namespace nu.xom
{
    public abstract class Node
    {
        public XNode XNode { get; private set; }

        protected Node(XNode xNode)
        {
            this.XNode = xNode;
        }

        public void detach()
        {
            this.XNode.Remove();
        }

        public Document getDocument()
        {
            return new Document(this.XNode.Document);
        }

        public Nodes query(string xPath)
        {
            return query(xPath, null);
        }

        public Nodes query(string xPath, XPathContext namespaces)
        {
            IList<XElement> descendantElements = new List<XElement>();
            if (!(this.XNode is XContainer)) return new Nodes(descendantElements);

            var xContainer = this.XNode as XContainer;
            var name = GetName(xPath);
            foreach (var element in xContainer.Descendants())
            {
                if (element.Name.LocalName.Equals(name))
                {
                    descendantElements.Add(element);
                }
            }
            return new Nodes(descendantElements);
        }

        public string toXML()
        {
            return this.XNode.ToString(SaveOptions.None);
        }

        public ParentNode getParent()
        {
            if (this.XNode.Parent is XElement)
            {
                return new Element(this.XNode.Parent as XElement);
            }
            return null;
        }

        public bool @equals(object obj)
        {
            if (obj == null) return false;
            if (this == obj) return true;

            var node = obj as Node;
            if (node == null) return false;

            return this.XNode.Equals(node.XNode);
        }

        public int hashCode()
        {
            return this.XNode.GetHashCode();
        }

        private static string GetName(string xPath)
        {
            if (xPath.LastIndexOf(":") != -1)
            {
                return xPath.Substring(xPath.LastIndexOf(":") + 1);
            }
            else
            {
                return xPath.Substring(xPath.LastIndexOf("/") + 1);
            }
        }
    }
}
