using System;
using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertTrue
{
    [ConcordionTest]
    public class AssertTrueTest
    {
        public string successOrFailure(string fragment, string evaluationResult)
        {
            return new TestRig()
                .WithStubbedEvaluationResult(Boolean.Parse(evaluationResult))
                .ProcessFragment(fragment)
                .SuccessOrFailureInWords();
        }
    }
}
