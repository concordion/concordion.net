using Concordion.NET.Integration;

namespace Concordion.Spec.Examples
{
    [ConcordionTest]
    public class DemoTest
    {
        public string greetingFor(string firstName)
        {
            return string.Format("Hello {0}!", firstName);
        }
    }
}
