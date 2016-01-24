using Concordion.NET.Integration;
using Concordion.Spec.Support;
using nu.xom;

namespace Concordion.Spec.Concordion.Command.Results.Stylesheet
{
    [ConcordionTest]
    public class MissingHeadElementTest
    {
        public string process(string html)
        {
            var rootElement = new TestRig()
                                    .Process(html)
                                    .GetDocument()
                                    .getRootElement();
            RemoveIrrelevantElements(rootElement);
            return rootElement.toXML();
        }

        private void RemoveIrrelevantElements(Element rootElement)
        {
            RemoveIrrelevantStylesheet(rootElement);
            RemoveIrrelevantMetadata(rootElement);
            RemoveIrrelevantFooter(rootElement);
        }

        private void RemoveIrrelevantStylesheet(Element rootElement)
        {
            var head = rootElement.getFirstChildElement("head");
            var style = head.getFirstChildElement("style");
            head.removeChild(style);
        }

        private void RemoveIrrelevantMetadata(Element rootElement)
        {
            var head = rootElement.getFirstChildElement("head");
            var meta = head.getFirstChildElement("meta");
            head.removeChild(meta);
        }

        private void RemoveIrrelevantFooter(Element rootElement)
        {
            var body = rootElement.getFirstChildElement("body");
            var footer = body.getFirstChildElement("div");
            body.removeChild(footer);
        }
    }
}
