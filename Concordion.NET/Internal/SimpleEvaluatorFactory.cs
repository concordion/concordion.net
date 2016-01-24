using org.concordion.api;

namespace Concordion.NET.Internal
{
    public class SimpleEvaluatorFactory : EvaluatorFactory
    {
        #region IEvaluatorFactory Members

        public Evaluator createEvaluator(object fixture)
        {
            return new SimpleEvaluator(fixture);
        }

        #endregion
    }
}
