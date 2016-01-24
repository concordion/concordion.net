using System;
using System.IO;
using System.Linq;
using Concordion.NET.Integration;
using Concordion.NET.Internal;
using Concordion.NET.Internal.Runner;

namespace Concordion.Spec.Concordion.Configuration
{
    [ConcordionTest]
    public class BaseOutputDirectoryTest
    {
        private static bool m_InTestRun = false;

        public void WithTargetDirectory(string baseOutputDirectory)
        {
            if (m_InTestRun) return;

            m_InTestRun = true;

            try
            {
                var reportFilePath = baseOutputDirectory + 
                    "\\Concordion\\Spec\\Concordion\\Configuration\\BaseOutputDirectory.html";
                if (File.Exists(reportFilePath))
                {
                    File.Delete(reportFilePath);
                }
                var specificationConfig = new SpecificationConfig().Load(this.GetType());
                specificationConfig.BaseOutputDirectory = baseOutputDirectory;
                var testRunner = new DefaultConcordionRunner();
                var testResult = testRunner.Run(this, specificationConfig);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception during test execution: {0}", e);
            }
            finally
            {
                m_InTestRun = false;
            }
        }

        public bool IsReportLocatedIn(string baseOutputDirectory)
        {
            if (m_InTestRun) return true;

            if (!Directory.Exists(baseOutputDirectory)) return false;
            var fileSystemEntries = Directory.GetFileSystemEntries(baseOutputDirectory +
                "\\Concordion\\Spec\\Concordion\\Configuration");
            return fileSystemEntries.Any(fileSystemEntry => fileSystemEntry.Contains("BaseOutputDirectory.html"));
        }
    }
}
