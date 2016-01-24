using System;
using System.Collections.Generic;
using System.Linq;
using Concordion.Test.Support;
using NUnit.Framework;
using org.concordion.@internal.listener;
using nu.xom;
using Attribute = nu.xom.Attribute;

namespace Concordion.Test.Listener
{
    [TestFixture]
    public class MetadataCreatorTest
    {
        private MetadataCreator metadataCreator;
        private Element html; 
        private Document document;
        private Element head;
    
        [SetUp]
        public void Init() {
            html = new Element("html");
            head = new Element("head");
            html.appendChild(head);
            document = new Document(html);
            metadataCreator = new MetadataCreator();
        }
    
        [Test]
        public void AddsContentTypeMetadataIfMissing() {
            metadataCreator.beforeParsing(document);
            Assert.AreEqual(
                "<html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" /></head></html>",
                new HtmlUtil().RemoveWhitespaceBetweenTags(html.toXML()));
        }

        [Test]
        public void DoesNotAddContentTypeMetadataIfAlreadyPresent() {
            var meta = new Element("meta");
            meta.addAttribute(new Attribute("http-equiv", "Content-Type"));
            meta.addAttribute(new Attribute("content", "text/html; charset=UTF-8"));
            head.appendChild(meta);
            Assert.AreEqual(
                "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /></head></html>",
                new HtmlUtil().RemoveWhitespaceBetweenTags(html.toXML()));
            metadataCreator.beforeParsing(document);
            Assert.AreEqual(
                "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /></head></html>",
                new HtmlUtil().RemoveWhitespaceBetweenTags(html.toXML()));
        }
    }
}
