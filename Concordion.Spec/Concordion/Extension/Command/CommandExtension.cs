using System.IO;
using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Command
{
    public class CommandExtension : ConcordionExtension
    {
        private readonly TextWriter m_LogWriter;

        public CommandExtension(TextWriter logWriter)
        {
            this.m_LogWriter = logWriter;
        }

        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withCommand("http://myorg.org/my/extension", "log", new LogCommand(this.m_LogWriter));
        }
    }
}
