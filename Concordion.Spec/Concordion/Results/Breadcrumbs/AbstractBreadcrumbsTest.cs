using System;
using org.concordion.api;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Results.Breadcrumbs
{
    public abstract class AbstractBreadcrumbsTest
    {
        private TestRig testRig = new TestRig();

        public virtual void setUpResource(string resourceName, string content) 
        {
            testRig.WithResource(new Resource(resourceName), content);
        }

        public virtual Result getBreadcrumbsFor(string resourceName) 
        {
            var spanElements = testRig
                .Process(new Resource(resourceName))
                .GetRootElement()
                .getDescendantElements("span");
            
            Result result = new Result();
            foreach (var span in spanElements) 
            {
                if ("breadcrumbs" == span.getAttributeValue("class")) 
                {
                    result.html = span.toXML()
                        .Replace("\r\n", String.Empty)
                        .Replace(">  <", "><");
                    result.text = span.getText();
                }
            }
            return result;
        }
    
        public class Result 
        {
            public string text = "";
            public string html = "";
        }
    }
}
