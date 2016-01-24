using org.concordion.api;

namespace Concordion.NET.Internal
{
    public class BridgingEvaluatorFactory : EvaluatorFactory
    {
        #region IEvaluatorFactory Members

        public Evaluator createEvaluator(object fixture)
        {
            return new BridgingEvaluator(fixture);
        }

        #endregion
    }
}
