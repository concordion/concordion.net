using System.IO;
using Concordion.NET.Integration;
using Concordion.NET.Internal.Runner;
using Concordion.NET.Internal;

namespace Concordion.Spec.Concordion.Configuration
{
    [ConcordionTest]
    public class BaseInputDirectoryTest
    {
        private static bool m_InTestRun = false;

        public bool DirectoryBasedExecuted(string baseInputDirectory)
        {
            if (m_InTestRun) return true;

            m_InTestRun = true;

            //work around for problem of NUnit GUI runner
            baseInputDirectory = baseInputDirectory +
                                 Path.DirectorySeparatorChar +
                                 ".." +
                                 Path.DirectorySeparatorChar +
                                 this.GetType().Assembly.GetName().Name;

            var specificationConfig = new SpecificationConfig().Load(this.GetType());
            specificationConfig.BaseInputDirectory = baseInputDirectory;
            var testRunner = new DefaultConcordionRunner();
            var testResult = testRunner.Run(this, specificationConfig);

            m_InTestRun = false;

            return testResult.getFailureCount() == 0 && !testResult.hasExceptions();
        }

        public bool EmbeddedExecuted()
        {
            if (m_InTestRun) return true;

            m_InTestRun = true;

            var specificationConfig = new SpecificationConfig().Load(this.GetType());
            specificationConfig.BaseInputDirectory = null;
            var fixtureRunner = new DefaultConcordionRunner();
            var testResult = fixtureRunner.Run(this, specificationConfig);

            m_InTestRun = false;

            bool hasFailures = testResult.getFailureCount() == 0;
            bool hasExceptions = !testResult.hasExceptions();
            return hasFailures && hasExceptions;
        }
    }
}
