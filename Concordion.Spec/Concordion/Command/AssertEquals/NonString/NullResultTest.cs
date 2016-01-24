using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertEquals.NonString
{
    [ConcordionTest]
    public class NullResultTest
    {
        public string outcomeOfPerformingAssertEquals(string snippet, string expectedString, string result)
        {
            if (result.Equals("null"))
            {
                result = null;
            }

            return new TestRig()
                .WithStubbedEvaluationResult(result)
                .ProcessFragment(snippet.Replace("(some expectation)", expectedString))
                .SuccessOrFailureInWords();
        }
    }
}
