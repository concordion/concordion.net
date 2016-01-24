using System;
using System.Collections.Generic;
using Concordion.NET.Integration;
using Concordion.Spec.Support;
using org.concordion.@internal.util;
using nu.xom;

namespace Concordion.Spec.Concordion.Command.VerifyRows
{
    [ConcordionTest]
    public class VerifyRowsTest
    {
        public ICollection<string> usernames = new List<string>();

        public string processFragment(string fragment, string csv)
        {
            usernames = csvToCollection(csv);
            Document document = new TestRig()
                .WithFixture(this)
                .ProcessFragment(fragment)
                .GetDocument();

            var result = String.Empty;

            var table = (Element) document.getRootElement().query("//table").get(0);
            var rows = table.query(".//tr");
            for (int i = 1; i < rows.size(); i++) {
                if (!result.Equals("")) {
                    result += ", ";
                }
                result += categorize((Element)rows.get(i));
            }

            return result;
        }

        private string categorize(Element row)
        {
            var cssClass = row.getAttributeValue("class");
            if (cssClass == null)
            {
                var cell = (Element) row.query("td").get(0);
                cssClass = cell.getAttributeValue("class");
            }
            Check.notNull(cssClass, "cssClass is null");
            return cssClass.ToUpper();
        }

        private static ICollection<string> csvToCollection(string csv) 
        {
            ICollection<string> c = new List<string>();
            foreach (string s in csv.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)) 
            {
                c.Add(s);
            }
            return c;
        }
    }
}
