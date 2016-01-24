using System.Collections.Generic;
using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.VerifyRows.Results
{
    [ConcordionTest]
    public class MissingRowsTest
    {
        private List<Person> people = new List<Person>();

        public void addPerson(string firstName, string lastName, int birthYear)
        {
            people.Add(new Person(firstName, lastName, birthYear));
        }

        public string getOutputFragment(string inputFragment)
        {
            var document = new TestRig()
                                .WithFixture(this)
                                .ProcessFragment(inputFragment)
                                .GetDocument();

            var tables = document.query("//table").get(0);

            return tables.toXML().Replace("\u00A0", "&#160;");
        }

        public ICollection<Person> getPeople()
        {
            return people;
        }

        public class Person
        {

            public Person(string firstName, string lastName, int birthYear)
            {
                this.firstName = firstName;
                this.lastName = lastName;
                this.birthYear = birthYear;
            }

            public string firstName;
            public string lastName;
            public int birthYear;
        }
    }
}
