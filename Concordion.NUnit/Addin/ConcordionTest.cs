using System;
using System.Reflection;
using Concordion.NET.Internal.Runner;
using NUnit.Core;
using org.concordion.api;

namespace Concordion.NUnit.Addin
{
    public class ConcordionTest : Test
    {
        #region Fields

        private readonly Type m_FixtureType;

        private readonly MethodInfo[] m_FixtureSetUpMethods;

        private readonly MethodInfo[] m_FixtureTearDownMethods;

        #endregion

        #region Constructors

        public ConcordionTest(Type fixtureType)
            : base(string.Format("Executable Specification: {0}", fixtureType.Name))
        {
            this.m_FixtureType = fixtureType;

            this.m_FixtureSetUpMethods =
                Reflect.GetMethodsWithAttribute(fixtureType, NUnitFramework.FixtureSetUpAttribute, true);
            this.m_FixtureTearDownMethods =
                Reflect.GetMethodsWithAttribute(fixtureType, NUnitFramework.FixtureTearDownAttribute, true);
        }

        #endregion

        #region Overrides of Test

        public override TestResult Run(EventListener listener, ITestFilter filter)
        {
            listener.TestStarted(this.TestName);

            Fixture = Reflect.Construct(m_FixtureType);
            RunFixtureSetUp();
            TestResult testResult;
            try
            {
                var testRunner = new DefaultConcordionRunner();
                var concordionResult = testRunner.Run(Fixture);
                testResult = NUnitTestResult(concordionResult, "");
            }
            catch (Exception exception)
            {
                testResult = new TestResult(this);
                testResult.Error(exception);
            }
            RunFixtureTearDown();

            listener.TestFinished(testResult);

            return testResult;
        }

        public override string TestType
        {
            get { return "ConcordionTest"; }
        }

        public sealed override object Fixture { get; set; }

        #endregion

        #region private methods

        private void RunFixtureSetUp()
        {
            if (m_FixtureSetUpMethods != null)
            {
                foreach (MethodInfo setUpMethod in m_FixtureSetUpMethods)
                    Reflect.InvokeMethod(setUpMethod, setUpMethod.IsStatic ? null : Fixture);
            }
        }

        private void RunFixtureTearDown()
        {
            if (m_FixtureTearDownMethods != null)
            {
                foreach (MethodInfo tearDownMethod in m_FixtureTearDownMethods)
                {
                    Reflect.InvokeMethod(tearDownMethod, tearDownMethod.IsStatic ? null : this.Fixture);
                }
            }
        }

        private TestResult NUnitTestResult(ResultSummary concordionResult, string resultPath)
        {
            var testResult = new TestResult(this);

            if (concordionResult.hasExceptions())
            {
                testResult.Error(new NUnitException("Exception in Concordion test: please see Concordion test reports"));
            }
            else if (concordionResult.getFailureCount() > 0)
            {
                testResult.Failure("Concordion Test Failures: " + concordionResult.getFailureCount(),
                                   "for stack trace, please see Concordion test reports");
            } else
            {
                testResult.Success(resultPath);
            }

            return testResult;
        }

        #endregion
    }
}
