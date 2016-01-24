using System;
using System.Collections.Generic;
using System.Linq;
using org.concordion.api.extension;
using org.concordion.api.listener;
using nu.xom;
using Element = org.concordion.api.Element;

namespace Concordion.Spec.Concordion.Extension.Configuration
{
    public class FakeExtensionBase : ConcordionExtension, DocumentParsingListener
    {
        public static readonly String FakeExtensionAttrName = "fake.extensions";
        private readonly string m_Text;

        public FakeExtensionBase() {
            this.m_Text = this.GetType().Name;
        }
    
        public FakeExtensionBase(string text) {
            this.m_Text = text;
        }

        public void beforeParsing(Document document)
        {
            var rootElement = new Element(document.getRootElement());
            var existingValue = rootElement.getAttributeValue(FakeExtensionAttrName);
            var newValue = this.m_Text;
            if (existingValue != null) {
                newValue = existingValue + ", " + newValue;
            }
            rootElement.addAttribute(FakeExtensionAttrName, newValue);
        }

        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withDocumentParsingListener(this);
        }
    }
}
