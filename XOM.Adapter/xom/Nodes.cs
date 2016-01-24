using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace nu.xom
{
    public class Nodes
    {
        private IEnumerable<XElement> m_XElements;

        public Nodes(IEnumerable<XElement> xElements)
        {
            this.m_XElements = xElements;
        }

        public Node get(int index)
        {
            return new Element(this.m_XElements.ElementAt(index));
        }

        public int size()
        {
            return this.m_XElements.Count();
        }
    }
}
