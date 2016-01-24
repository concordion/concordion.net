using System.Collections.Generic;
using Concordion.NET.Integration;
using Concordion.Spec.Support;
using java.lang;
using org.concordion.api;
using org.concordion.api.listener;
using org.concordion.@internal.listener;

namespace Concordion.Spec.Concordion.Results.Exception
{
    [ConcordionTest]
    public class ExceptionTest
    {
        private List<StackTraceElement> stackTraceElements = new List<StackTraceElement>();

        public void addStackTraceElement(string declaringClassName, string methodName, string filename, int lineNumber)
        {
            if (filename.Equals("null"))
            {
                filename = null;
            }
            stackTraceElements.Add(new StackTraceElement(declaringClassName, methodName, filename, lineNumber));
        }

        public string markAsException(string fragment, string expression, string errorMessage)
        {
            Throwable t = new Throwable(errorMessage);
            t.setStackTrace(stackTraceElements.ToArray());

            Element element = new Element((nu.xom.Element)new TestRig()
                .ProcessFragment(fragment)
                .GetDocument()
                .query("//p")
                .get(0));

            new ThrowableRenderer().throwableCaught(new ThrowableCaughtEvent(t, element, expression));

            return element.toXML();
        }
    }
}
