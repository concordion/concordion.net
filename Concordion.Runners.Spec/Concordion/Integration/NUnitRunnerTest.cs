using Concordion.Runners.NUnit;
using NUnit.Framework;

namespace Concordion.Runners.Spec.Concordion.Integration
{
    [TestFixture]
    public class NUnitRunnerTest : ExecutableSpecification
    {
        private readonly Greeter _helloWorldGreeter = new Greeter();

        public string GetGreeting()
        {
            return _helloWorldGreeter.GetMessage();
        }
    }

    public class Greeter
    {
        public string GetMessage()
        {
            return "Hello World!";
        }
    }
}
