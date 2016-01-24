using System.IO;
using org.concordion.api.listener;

namespace Concordion.Spec.Concordion.Extension.Listener
{
    public class AssertLogger : AssertEqualsListener, AssertTrueListener, AssertFalseListener
    {
        private readonly TextWriter m_LogWriter;

        public AssertLogger(TextWriter logWriter)
        {
            this.m_LogWriter = logWriter;
        }

        public void successReported(AssertSuccessEvent successEvent)
        {
            m_LogWriter.WriteLine("Success '{0}'", successEvent.getElement().getText());
        }

        public void failureReported(AssertFailureEvent failureEvent)
        {
            m_LogWriter.WriteLine("Failure expected:'{0}' actual:'{1}'", failureEvent.getExpected(), failureEvent.getActual());
        }
    }
}
