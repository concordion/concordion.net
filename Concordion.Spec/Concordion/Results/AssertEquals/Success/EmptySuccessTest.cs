using System;
using Concordion.NET.Integration;

namespace Concordion.Spec.Concordion.Results.AssertEquals.Success
{
    [ConcordionTest]
    public class EmptySuccessTest : SuccessTest
    {
        public EmptySuccessTest()
        {
            username = String.Empty;
        }
    }
}
