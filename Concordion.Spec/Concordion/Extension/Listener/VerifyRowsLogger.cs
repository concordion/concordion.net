using System.IO;
using org.concordion.api.listener;

namespace Concordion.Spec.Concordion.Extension.Listener
{
    public class VerifyRowsLogger : VerifyRowsListener
    {
        #region Fields

        private readonly TextWriter m_LogWriter;

        #endregion

        #region IVerifyRowsListener Members

        public VerifyRowsLogger(TextWriter logWriter)
        {
            this.m_LogWriter = logWriter;
        }

        public void expressionEvaluated(ExpressionEvaluatedEvent expressionEvaluatedEvent)
        {
            this.m_LogWriter.WriteLine("Evaluated '{0}'",
                                  expressionEvaluatedEvent.getElement().getAttributeValue(
                                  "verifyRows", "http://www.concordion.org/2007/concordion"));
        }

        public void missingRow(MissingRowEvent missingRowEvent)
        {
            this.m_LogWriter.WriteLine("Missing Row '{0}'", missingRowEvent.getRowElement().getText());
        }

        public void surplusRow(SurplusRowEvent surplusRowEvent)
        {
            this.m_LogWriter.WriteLine("Surplus Row '{0}'", surplusRowEvent.getRowElement().getText());
        }

        #endregion

    }
}
