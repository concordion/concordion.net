using System.IO;
using org.concordion.api.listener;

namespace Concordion.Spec.Concordion.Extension.Listener
{
    public class ExecuteLogger : ExecuteListener
    {
        private readonly TextWriter m_LogWriter;

        public ExecuteLogger(TextWriter logWriter)
        {
            this.m_LogWriter = logWriter;
        }

        public void executeCompleted(ExecuteEvent executeEvent)
        {
            var element = executeEvent.getElement();
            if (element.isNamed("tr"))
            {
                var stringWriter = new StringWriter();
                stringWriter.Write("Execute '");
                var childElements = element.getChildElements();
                bool firstChild = true;
                foreach (var childElement in childElements)
                {
                    if (firstChild)
                    {
                        firstChild = false;
                    }
                    else
                    {
                        stringWriter.Write(", ");
                    }
                    stringWriter.Write(childElement.getText());
                }
                stringWriter.Write("'");
                m_LogWriter.WriteLine(stringWriter.ToString());
            }
            else
            {
                m_LogWriter.WriteLine("Execute '{0}'", element.getText());
            }
        }
    }
}
