using System;
using Concordion.NET.Integration;
using Concordion.Spec.Support;
using org.concordion.api;

namespace Concordion.Spec.Concordion.Command.Results.Stylesheet
{
    [ConcordionTest]
    public class StylesheetTest
    {
        private Element outputDocument;

        public void processDocument(string html)
        {
            outputDocument = new TestRig()
                .Process(html)
                .GetRootElement();
        }

        public string getRelativePosition(string outer, string target, string sibling)
        {
            var outerElement = outputDocument.getFirstDescendantNamed(outer);

            int targetIndex = indexOfFirstChildWithName(outerElement, target);
            int siblingIndex = indexOfFirstChildWithName(outerElement, sibling);

            return targetIndex > siblingIndex ? "after" : "before";
        }

        private int indexOfFirstChildWithName(Element element, string name) 
        {
            int index = 0;
            foreach (var e in element.getChildElements()) 
            {
                if (e.getLocalName().Equals(name)) 
                {
                    return index;
                }
                index++;
            }
            throw new Exception("No child <" + name + "> found.");
        }

        public bool elementTextContains(string elementName, string s1, string s2)
        {
            var element = outputDocument.getRootElement().getFirstDescendantNamed(elementName);
            string text = element.getText();
            return text.Contains(s1) && text.Contains(s2);
        }
    }
}
