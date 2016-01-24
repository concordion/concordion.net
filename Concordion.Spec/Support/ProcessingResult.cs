using System;
using System.Collections.Generic;
using System.Linq;
using org.concordion.api;
using org.concordion.api.listener;
using nu.xom;
using org.concordion.@internal;
using Element = org.concordion.api.Element;

namespace Concordion.Spec.Support
{
    public class ProcessingResult
    {
        private readonly ResultSummary resultSummary;
        private readonly EventRecorder eventRecorder;
        private readonly string documentXML;

        public long SuccessCount
        {
            get
            {
                return this.resultSummary.getSuccessCount();
            }
        }

        public long FailureCount
        {
            get
            {
                return this.resultSummary.getFailureCount();
            }
        }

        public long ExceptionCount
        {
            get
            {
                return this.resultSummary.getExceptionCount();
            }
        }

        public bool HasFailures
        {
            get
            {
                return this.FailureCount + this.ExceptionCount != 0;
            }
        }

        public bool IsSuccess
        {
            get
            {
                return !this.HasFailures;
            }
        }

        public ProcessingResult(ResultSummary resultSummary, EventRecorder eventRecorder, string documentXML) 
        {
            this.resultSummary = resultSummary;
            this.eventRecorder = eventRecorder;
            this.documentXML = documentXML;
        }

        public string SuccessOrFailureInWords()
        {
            return this.HasFailures ? "FAILURE" : "SUCCESS";
        }

        public Element GetOutputFragment()
        {
            return this.GetRootElement().getFirstDescendantNamed("fragment");
        }

        public string GetOutputFragmentXML()
        {
            return this.GetOutputFragment().toXML()
                .Replace("<fragment>", String.Empty)
                .Replace("</fragment>", String.Empty)
                .Replace("\u00A0", "&#160;")
                .Replace(" xmlns:concordion=\"http://www.concordion.org/2007/concordion\"", String.Empty);
        }

        public Document GetDocument()
        {
            return XMLParser.parse(this.documentXML);
        }

        public AssertFailureEvent GetLastAssertEqualsFailureEvent()
        {
            return this.eventRecorder.GetLast(typeof(AssertFailureEvent)) as AssertFailureEvent;
        }

        public Element GetRootElement()
        {
            return new Element(this.GetDocument().getRootElement());
        }

        public bool HasCssDeclaration(string cssFilename)
        {
            var head = this.GetRootElement().getFirstChildElement("head");
            return head.getChildElements("link").Any(
                link =>
                    string.Equals("text/css", link.getAttributeValue("type")) &&
                    string.Equals("stylesheet", link.getAttributeValue("rel")) &&
                    string.Equals(cssFilename, link.getAttributeValue("href")));
        }

        public bool HasEmbeddedCss(string css)
        {
            var head = this.GetRootElement().getFirstChildElement("head");
            return head.getChildElements("style").Any(style => style.getText().Contains(css));
        }

        public bool HasJavaScriptDeclaration(string cssFilename) {
            var head = this.GetRootElement().getFirstChildElement("head");
            return head.getChildElements("script").Any(
                script =>
                    string.Equals("text/javascript", script.getAttributeValue("type")) &&
                    string.Equals(cssFilename, script.getAttributeValue("src")));
        }

        public bool HasEmbeddedJavaScript(string javaScript) {
            var head = this.GetRootElement().getFirstChildElement("head");
            return head.getChildElements("script").Any(
                script =>
                    string.Equals("text/javascript", (string)script.getAttributeValue("type")) &&
                    script.getText().Contains(javaScript));
        }
    }
}
