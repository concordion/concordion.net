using System;
using System.Globalization;
using Concordion.NET.Integration;
using Concordion.Spec.Support;

namespace Concordion.Spec.Concordion.Command.AssertEquals.NonString
{
    [ConcordionTest]
    public class NonStringTest
    {
        public string outcomeOfPerformingAssertEquals(string fragment, string expectedString, string result, string resultType)
        {
            object simulatedResult;
            if (resultType.Equals("String"))
            {
                simulatedResult = result;
            }
            else if (resultType.Equals("Integer"))
            {
                simulatedResult = Int32.Parse(result);
            }
            else if (resultType.Equals("Double"))
            {
                var customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                simulatedResult = Double.Parse(result);
            }
            else
            {
                throw new Exception("Unsupported result-type '" + resultType + "'.");
            }

            return new TestRig()
                .WithStubbedEvaluationResult(simulatedResult)
                .ProcessFragment(fragment.Replace("(some expectation)", expectedString))
                .SuccessOrFailureInWords();
        }
    }
}
