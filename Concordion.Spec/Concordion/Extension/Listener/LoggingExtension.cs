using System.IO;
using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Listener
{
    public class LoggingExtension : ConcordionExtension
    {
        private readonly AssertLogger m_AssertLogger;

        private readonly ExecuteLogger m_ExecuteLogger;

        private readonly VerifyRowsLogger m_VerifyRowsLogger;

        public LoggingExtension(TextWriter logWriter)
        {
            this.m_AssertLogger = new AssertLogger(logWriter);
            this.m_ExecuteLogger = new ExecuteLogger(logWriter);
            this.m_VerifyRowsLogger = new VerifyRowsLogger(logWriter);
        }

        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withAssertEqualsListener(m_AssertLogger);
            concordionExtender.withAssertTrueListener(m_AssertLogger);
            concordionExtender.withAssertFalseListener(m_AssertLogger);
            concordionExtender.withExecuteListener(m_ExecuteLogger);
            concordionExtender.withVerifyRowsListener(m_VerifyRowsLogger);
        }
    }
}
