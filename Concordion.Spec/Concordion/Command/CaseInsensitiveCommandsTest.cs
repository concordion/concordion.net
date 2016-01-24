using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command
{
    [ConcordionTest]
    public class CaseInsensitiveCommands
    {
        public string process(string snippet, object stubbedResult)
        {
            long successCount = new TestRig()
                .WithStubbedEvaluationResult(stubbedResult)
                .ProcessFragment(snippet)
                .SuccessCount;
            
            return successCount == 1 ? snippet : "Did not work";
        }
    }
}
