using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Resource
{
    public class ResourceExtension : ConcordionExtension
    {
        public static readonly string SourcePath = "/test/concordion/o.png";

        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withResource(
                                    SourcePath,
                                    new org.concordion.api.Resource(("/images/o.png")));
        }
    }
}
