using org.concordion.api;
using org.concordion.@internal;

namespace Concordion.Spec.Concordion.Command.Run
{
    public class RunTestRunner : Runner
    {
        public static Result Result;

        public ResultSummary execute(Resource resource, string href)
        {
            return new SingleResultSummary(Result);
	    }
    }
}
