using Concordion.NET.Api.Extension;
using Concordion.NET.Integration;

namespace Concordion.Spec.Concordion.Extension.FileSuffix
{
    [ConcordionTest]
    [Extensions(typeof(XhtmlExtension))]
    public class FileSuffixExtensionsTest
    {
        public bool hasBeenProcessed()
        {
            return true;
        }
    }
}
