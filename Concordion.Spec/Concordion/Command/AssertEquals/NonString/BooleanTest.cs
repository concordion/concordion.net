using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertEquals.NonString
{
    [ConcordionTest]
    public class BooleanTest
    {
        public string OutcomeOfPerformingAssertEquals(string fragment, bool boolValue, string boolString)
        {
            return new TestRig()
                .WithStubbedEvaluationResult(new java.lang.Boolean(boolValue.ToString()))
                .ProcessFragment(fragment.Replace("(some boolean string)", boolString))
                .SuccessOrFailureInWords();
        }
    }
}
