using System;
using System.Text.RegularExpressions;
using Concordion.NET.Integration;

namespace Concordion.Spec.Concordion.Results.Breadcrumbs
{
    [ConcordionTest]
    public class WordingTest : AbstractBreadcrumbsTest
    {
        public string getBreadcrumbWordingFor(string resourceName, string content) 
        {
            string packageName = "/" + resourceName.Replace(".html", String.Empty) + "/";
            string otherResourceName = "Demo.html";
            setUpResource(packageName + resourceName, content);
            setUpResource(packageName + otherResourceName, "<html />");
            var breadcrumbs = getBreadcrumbsFor(packageName + otherResourceName).text;
            return Regex.Replace(breadcrumbs, " *> *", String.Empty);
        }
    }
}
