using System.IO;
using org.concordion.api;

namespace Concordion.Spec.Concordion.Extension.Command
{
    public class LogCommand : org.concordion.api.Command
    {
        private TextWriter LogWriter { get; set; }

        public LogCommand(TextWriter logWriter)
        {
            this.LogWriter = logWriter;
        }

        public void setUp(CommandCall commandCall, Evaluator evaluator, ResultRecorder resultRecorder)
        {
        }

        public void execute(CommandCall commandCall, Evaluator evaluator, ResultRecorder resultRecorder)
        {
            this.LogWriter.WriteLine(commandCall.getElement().getText());
        }

        public void verify(CommandCall commandCall, Evaluator evaluator, ResultRecorder resultRecorder)
        {
        }
    }
}
