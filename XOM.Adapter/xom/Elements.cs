using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace nu.xom
{
    public class Elements
    {
        private IEnumerable<XElement> m_XElements; 

        public Elements(IEnumerable<XElement> xElements)
        {
            this.m_XElements = xElements;
        }

        public int size()
        {
            return this.m_XElements.Count();
        }

        public Element get(int index)
        {
            return new Element(this.m_XElements.ElementAt(index));
        }
    }
}
