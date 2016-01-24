using Concordion.NET.Integration;
using NUnit.Framework;

namespace Concordion.Spec.Concordion.Attributes
{
    //This examples shows how to exclude a Concordion.NET test from execution by using the NUnit [Ignore] attribute.
    //Please, comment in the following two lines containing the attributes - it has been commented out to prevent a yellow line when running all Concordion.NET tests.
    [Ignore]
    [ConcordionTest]
    public class ExampleIgnoreTest
    {
    }
}
