using System;
using NUnit.Framework;

namespace Concordion.NET.Integration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConcordionTestAttribute : TestFixtureAttribute
    {
        public const string AttributeIdentifier = "Concordion.NET.Integration.ConcordionTestAttribute";

        public string Name
        {
            get;
            private set;
        }

        public string Path
        {
            get;
            private set;
        }
    }
}
