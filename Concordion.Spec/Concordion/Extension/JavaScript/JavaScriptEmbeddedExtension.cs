using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.JavaScript
{
    public class JavaScriptEmbeddedExtension : ConcordionExtension
    {
        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withEmbeddedJavaScript(JavaScriptExtensionTest.TestJs);
        }
    }
}
