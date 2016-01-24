using Concordion.NET.Integration;
using Concordion.Spec.Support;
using org.concordion.api;

namespace Concordion.Spec.Concordion.Command.Run
{
    [ConcordionTest]
    public class RunTest
    {
    	public string successOrFailure(string fragment, string hardCodedTestResult, string evaluationResult)
    	{
            if ("SUCCESS".Equals(hardCodedTestResult))
    	    {
    	        RunTestRunner.Result = Result.SUCCESS;   
    	    }
    	    if ("FAILURE".Equals(hardCodedTestResult))
    	    {
    	        RunTestRunner.Result = Result.FAILURE;
    	    }

            return new TestRig()
                .WithStubbedEvaluationResult(evaluationResult)
                .ProcessFragment(fragment)
                .SuccessOrFailureInWords();
        }
    }
}
