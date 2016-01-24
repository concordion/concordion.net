using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertEquals
{
    [ConcordionTest]
    public class SupportedElementsTest
    {
        public string process(string snippet)
        {
            long successCount = new TestRig()
                .WithStubbedEvaluationResult("Fred")
                .ProcessFragment(snippet)
                .SuccessCount;
            
            return successCount == 1 ? snippet : "Did not work";
        }
    }
}
