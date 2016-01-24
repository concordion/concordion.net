using Concordion.Test.Support;
using NUnit.Framework;
using org.concordion.@internal.listener;
using nu.xom;

namespace Concordion.Test.Listener
{
    [TestFixture]
    public class DocumentStructureImproverTest
    {
        private DocumentStructureImprover improver;
        private Element html;
        private Document document;

        [SetUp]
        public void Init()
        {
            improver = new DocumentStructureImprover();
            html = new Element("html");
            document = new Document(html);
        }
    
        [Test]
        public void AddsHeadIfMissing()
        {
            improver.beforeParsing(document);
            Assert.AreEqual(
                "<html><head /></html>",
                new HtmlUtil().RemoveWhitespaceBetweenTags(html.toXML()));

            // Check it does not add it again if we repeat the call
            improver.beforeParsing(document);
            Assert.AreEqual(
                "<html><head /></html>", 
                new HtmlUtil().RemoveWhitespaceBetweenTags(html.toXML()));
        }

        [Test]
        public void TransfersEverythingBeforeBodyIntoNewlyCreatedHead()
        {
            var style1 = new Element("style1");
            var style2 = new Element("style2");
            html.appendChild(style1);
            html.appendChild(style2);

            var body = new Element("body");
            body.insertChild("some ", 0);
            var bold = new Element("b");
            bold.insertChild("bold text", 0);
            body.appendChild(bold);
            html.appendChild(body);
            improver.beforeParsing(document);

            Assert.AreEqual(
                "<html><head><style1 /><style2 /></head><body>some <b>bold text</b></body></html>",
                new HtmlUtil().RemoveWhitespaceBetweenTags(html.toXML()));
        }
    }
}
