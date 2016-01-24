using org.concordion.api.extension;

namespace Concordion.Spec.Concordion.Extension.JavaScript
{
    class JavaScriptLinkedExtension : ConcordionExtension
    {
        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withLinkedJavaScript(
                                    JavaScriptExtensionTest.SourcePath, 
                                    new org.concordion.api.Resource("/js/my.js"));
        }
    }
}
