using System;
using System.Collections.Generic;
using System.Reflection;
using Concordion.NET.Internal.Extension;
using org.concordion.api;
using org.concordion.@internal;
using File = java.io.File;

namespace Concordion.NET.Internal.Runner
{
    public class DefaultConcordionRunner : org.concordion.api.Runner
    {
        #region Properties

        protected static readonly Dictionary<Assembly, SpecificationConfig> FixtureAssemplies = 
            new Dictionary<Assembly, SpecificationConfig>();

        #endregion

        #region Methods

        public ResultSummary Run(object fixture)
        {
            return Run(fixture, this.GetConfig(fixture));
        }

        public ResultSummary Run(object fixture, SpecificationConfig specificationConfig)
        {
            var source = string.IsNullOrEmpty(specificationConfig.BaseInputDirectory)
                ? (Source)new EmbeddedResourceSource(fixture.GetType().Assembly)
                : new FileSource(fixture.GetType().Assembly, specificationConfig.BaseInputDirectory);

            var target = new FileTarget(new File(specificationConfig.BaseOutputDirectory));

            var testSummary = new SummarizingResultRecorder();
            var anySpecExecuted = false;

            var fileExtensions = specificationConfig.SpecificationFileExtensions;
            foreach (var fileExtension in fileExtensions)
            {
                var specLocator = new ClassNameBasedSpecificationLocator(fileExtension);
                var specResource = specLocator.locateSpecification(fixture);
                if (source.canFind(specResource))
                {
                    var fixtureResult = RunSingleSpecification(fixture, source, specLocator, target, specificationConfig);
                    AddToTestResults(fixtureResult, testSummary);
                    anySpecExecuted = true;
                }
            }
            if (anySpecExecuted) return testSummary;

            
            //string specPath;
            //if (!string.IsNullOrEmpty(m_SpecificationConfig.BaseInputDirectory))
            //{
            //    specPath = string.Format("directory {0}",
            //        Path.GetFullPath(m_SpecificationConfig.BaseInputDirectory));
            //}
            //else
            //{
            //    specPath = string.Format("assembly {0}",
            //        m_Fixture.GetType().Assembly.GetName().Name);
            //}
            //testSummary.record(org.concordion.api.Result.EXCEPTION);
            //testSummary.Error(new AssertionErrorException(string.Format(
            //    "no active specification found for {0} in {1}",
            //    this.m_Fixture.GetType().Name,
            //    specPath)));

            throw new InvalidOperationException(string.Format("no specification extensions defined for: {0}", specificationConfig));
        }

        private SpecificationConfig GetConfig(object fixture)
        {
            SpecificationConfig result;
            var fixtureAssembly = fixture.GetType().Assembly;
            FixtureAssemplies.TryGetValue(fixtureAssembly, out result);
            if (result == null)
            {
                result = new SpecificationConfig().Load(fixtureAssembly);
                FixtureAssemplies.Add(fixtureAssembly, result);
            }
            return result;
        }

        private ResultSummary RunSingleSpecification(object fixture, Source source, 
            SpecificationLocator specificationLocator, FileTarget target, SpecificationConfig specificationConfig)
        {
            var concordionExtender = new ConcordionBuilder();
            concordionExtender
                .withSource(source)
                .withTarget(target)
                .withSpecificationLocator(specificationLocator)
                .withEvaluatorFactory(new SimpleEvaluatorFactory());
            var extensionLoader = new ExtensionLoader(specificationConfig);
            extensionLoader.AddExtensions(fixture, concordionExtender);

            var concordion = concordionExtender.build();
            return concordion.process(specificationLocator.locateSpecification(fixture), fixture);
        }

        private void AddToTestResults(ResultSummary singleResult, ResultRecorder resultSummary)
        {
            if (resultSummary == null) return;

            if (singleResult.hasExceptions())
            {
                //resultSummary.AddResultDetails(singleResult.ErrorDetails);
                resultSummary.record(singleResult);
            }
            else if (singleResult.getFailureCount() > 0)
            {
                //resultSummary.AddResultDetails(singleResult.FailureDetails);
                resultSummary.record(singleResult);
            }
            else
            {
                //ToDo: resultSummary..Success();
                resultSummary.record(singleResult);
            }
        }

        private object GetFixture(Resource resource, string specificationPath)
        {
            var fixtureName = GetFixtureName(resource, specificationPath);
            var fixtureAssembly = GetAssembly(fixtureName);
            if (fixtureAssembly != null)
            {
                return Activator.CreateInstance(GetFixtureType(fixtureAssembly, fixtureName));
            }

            return null;
        }

        private static string GetFixtureName(Resource resource, string specificationPath)
        {
            var fullSpecPath = resource.getRelativeResource(specificationPath.Replace("\\", "/"));
            var fixtureName = fullSpecPath.getPath();
            fixtureName = fixtureName.Replace("\\", ".");
            fixtureName = fixtureName.Replace("/", ".");
            if (fixtureName.EndsWith(".html"))
            {
                fixtureName = fixtureName.Remove(fixtureName.Length - 5, 5);
            }
            if (fixtureName.StartsWith("."))
            {
                fixtureName = fixtureName.Remove(0, 1);
            }
            return fixtureName;
        }

        private static Assembly GetAssembly(string fixtureName)
        {
            foreach (var testAssembly in FixtureAssemplies.Keys)
            {
                if (GetFixtureType(testAssembly, fixtureName) != null)
                {
                    return testAssembly;
                }
            }
            return null;
        }

        private static Type GetFixtureType(Assembly fixtureAssembly, string fixtureName)
        {
            if (fixtureAssembly.GetType(fixtureName + "Test", false, true) != null)
            {
                return fixtureAssembly.GetType(fixtureName + "Test", false, true);
            }

            if (fixtureAssembly.GetType(fixtureName + "Fixture", false, true) != null)
            {
                return fixtureAssembly.GetType(fixtureName + "Fixture", false, true);
            }

            return fixtureAssembly.GetType(fixtureName, false, true);
        }

        #endregion

        #region IRunner Members

        public ResultSummary execute(Resource resource, string specificationPath)
        {
            var fixture = GetFixture(resource, specificationPath);
            return this.Run(fixture);
        }

        #endregion
    }
}
