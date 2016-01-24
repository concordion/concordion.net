using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.Css
{
    public class CssEmbeddedExtension : ConcordionExtension
    {
        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withEmbeddedCSS(CssExtensionTest.TestCss);
        }
    }
}
