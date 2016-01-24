using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.Execute
{
    [ConcordionTest]
    public class AccessToLinkHrefTest
    {
        public bool fragmentSucceeds(string fragment) 
        {
            ProcessingResult result = new TestRig()
                .WithFixture(this)
                .ProcessFragment(fragment);
            
            return result.IsSuccess && result.SuccessCount > 0;
        }
    
        public string myMethod(string s) 
        {
            return s;
        }
    }
}
