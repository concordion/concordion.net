using System;
using System.Collections.Generic;
using System.Linq;
using Concordion.Test.Support;
using NUnit.Framework;
using org.concordion.api;
using org.concordion.@internal.listener;
using nu.xom;
using Element = nu.xom.Element;

namespace Concordion.Test.Listener
{
    [TestFixture]
    public class JavaScriptLinkerTest
    {
        private static readonly Resource NOT_NEEDED_PARAMETER = null;

        [Test]
        public void XmlOutputContainsAnExplicitEndTagForScriptElement()
        {
            var javaScriptLinker = new JavaScriptLinker(NOT_NEEDED_PARAMETER);

            var html = new Element("html");
            var head = new Element("head");
            html.appendChild(head);

            javaScriptLinker.beforeParsing(new Document(html));

            var expected = "<head><script type=\"text/javascript\"></script></head>";
            var actual = new HtmlUtil().RemoveWhitespaceBetweenTags(head.toXML());
            Assert.AreEqual(expected, actual);
        }
    }
}
