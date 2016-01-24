using Concordion.NET.Internal;
using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.FileSuffix
{
    public class XhtmlExtension : ConcordionExtension
    {
        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender
                .withSpecificationLocator(new ClassNameBasedSpecificationLocator("xhtml"));
        }
    }
}
