using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertEquals.NonString
{
    [ConcordionTest]
    public class VoidResultTest
    {
        public string process(string snippet)
        {
            ProcessingResult r = new TestRig()
                                        .WithFixture(this)
                                        .ProcessFragment(snippet);

            if (r.ExceptionCount != 0)
            {
                return "exception";
            }

            return r.SuccessOrFailureInWords();
        }

        public void myVoidMethod()
        {
        }
    }
}
