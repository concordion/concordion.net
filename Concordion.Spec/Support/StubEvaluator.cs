using System;
using Concordion.NET.Internal;
using org.concordion.api;

namespace Concordion.Spec.Support
{
    public class StubEvaluator : Evaluator, EvaluatorFactory
    {
        private object m_EvaluationResult;

        public StubEvaluator(object fixture)
        {
            this.Fixture = fixture;
        }

        public Evaluator createEvaluator(object fixture)
        {
            return this;
        }

        public object evaluate(string expression) 
        {
            if (this.m_EvaluationResult is Exception) throw (Exception) this.m_EvaluationResult;
            return BridgingEvaluator.ConvertToJavaTypes(this.m_EvaluationResult);
        }

        public object getVariable(string variableName)
        {
            return null;
        }

        public void setVariable(string variableName, object value)
        {
        }

        public object Fixture
        {
            get;
            private set;
        }

        public EvaluatorFactory WithStubbedResult(object evaluationResult)
        {
            this.m_EvaluationResult = evaluationResult;
            return this;
        }
    }
}