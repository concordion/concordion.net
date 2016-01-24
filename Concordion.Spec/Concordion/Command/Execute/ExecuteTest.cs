using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.Execute
{
    [ConcordionTest]
    public class ExecuteTest
    {
        private bool myMethodWasCalled = false;

        public bool myMethodWasCalledProcessing(string fragment)
        {
            new TestRig()
                .WithFixture(this)
                .ProcessFragment(fragment);
            return myMethodWasCalled;
        }

        public void myMethod()
        {
            myMethodWasCalled = true;
        }
    }
}
