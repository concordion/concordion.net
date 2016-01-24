using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Results.AssertEquals.Success
{
    [ConcordionTest]
    public class SuccessTest
    {
        public string username = "fred";
    
        public string renderAsSuccess(string fragment)
        {
            return new TestRig()
                .WithFixture(this)
                .ProcessFragment(fragment)
                .GetOutputFragmentXML()
                .Replace("\u00A0", "&#160;");
        }
    }
}
