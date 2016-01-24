using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Css
{
    public class CssLinkedExtension : ConcordionExtension
    {
        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withLinkedCSS(
                                    CssExtensionTest.SourcePath, 
                                    new org.concordion.api.Resource("/css/my.css"));
        }
    }
}