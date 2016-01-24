using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Results.AssertEquals.Failure
{
    [ConcordionTest]
    public class NestedElementsTest : FailureTest
    {
        public string matchOrNotMatch(string snippet, string evaluationResult)
        {
            return new TestRig()
                .WithStubbedEvaluationResult(evaluationResult)
                .ProcessFragment(snippet)
                .HasFailures ? "not match" : "match";
        }
    }
}
