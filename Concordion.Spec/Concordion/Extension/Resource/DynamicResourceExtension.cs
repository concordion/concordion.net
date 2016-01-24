using org.concordion.api.extension;
using org.concordion.api.listener;
using java.io;

namespace Concordion.Spec.Concordion.Extension.Resource
{
    public class DynamicResourceExtension : ConcordionExtension, ConcordionBuildListener
    {
        public static readonly string SourcePath = "/test/concordion/o.png";
        private org.concordion.api.Target m_Target;

        public void addTo(ConcordionExtender concordionExtender)
        {
            concordionExtender.withBuildListener(this);
        }

        public void concordionBuilt(ConcordionBuildEvent buildEvent)
        {
            this.m_Target = buildEvent.getTarget();
        
            this.createResourceInTarget();  // NOTE: normally this would be done during specification processing - eg in an AssertEqualsListener
        }

        private void createResourceInTarget()
        {
            this.m_Target.copyTo(new org.concordion.api.Resource("/resource/my.txt"), new StringBufferInputStream("success"));
        }
    }
}
