using System.Collections.Generic;
using System.Linq;
using Concordion.NET.Integration;

namespace Concordion.Spec.Concordion.Extension.Listener
{
    [ConcordionTest]
    public class VerifyRowsListenerTest : AbstractExtensionTestCase
    {
        public void addLoggingExtension()
        {
            Extension = new LoggingExtension(LogWriter);
        }

        public List<string> getGeorgeAndRingo()
        {
            var result = new string[] {"George Harrison", "Ringo Starr"};
            return result.ToList();
        }
    }
}
