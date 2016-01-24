using Concordion.NET.Integration;

namespace Concordion.Spec.Concordion.Results.Breadcrumbs
{
    [ConcordionTest]
    public class BreadcrumbsTest : AbstractBreadcrumbsTest
    {
        public override void setUpResource(string resourceName, string content) 
        {
            base.setUpResource(resourceName, content);
        }

        public override Result getBreadcrumbsFor(string resourceName)
        {
            return base.getBreadcrumbsFor(resourceName);
        }
    }
}
