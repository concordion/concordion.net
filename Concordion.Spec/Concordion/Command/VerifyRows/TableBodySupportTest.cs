using System;
using System.Collections.Generic;
using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.VerifyRows
{
    [ConcordionTest]
    public class TableBodySupportTest
    {
        private List<string> names = new List<string>();

        public void setUpNames(string namesAsCSV) 
        {
            foreach (string name in namesAsCSV.Split(new char[]{',', ' '}, StringSplitOptions.RemoveEmptyEntries))
            {
                names.Add(name);
            }
        }

        public List<string> getNames()
        {
            return names;
        }

        public string process(string inputFragment)
        {
            var document = new TestRig()
                                .WithFixture(this)
                                .ProcessFragment(inputFragment)
                                .GetDocument();

            var table = document.query("//table").get(0);

            return table.toXML().Replace("\u00A0", "&#160;");
        }
    }
}
