using ognl;
using org.concordion.api;
using org.concordion.@internal.util;

namespace Concordion.NET.Internal
{
    public class OgnlEvaluator : Evaluator
    {
        #region Properties

        public object Fixture
        {
            get;
            private set;
        }

        private OgnlContext OgnlContext
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public OgnlEvaluator(object fixture)
        {
            this.Fixture = fixture;
            this.OgnlContext = new OgnlContext();
        }

        #endregion

        #region Methods

        private void PutVariable(string rawVariableName, object value)
        {
            Check.isFalse(rawVariableName.StartsWith("#"), "Variable name passed to evaluator should not start with #");
            Check.isTrue(!rawVariableName.Equals("in"), "'%s' is a reserved word and cannot be used for variables names", rawVariableName);
            this.OgnlContext[rawVariableName] = value;
        }

        #endregion

        #region Evaluator Members

        public virtual object getVariable(string expression)
        {
            Check.isTrue(expression.StartsWith("#"), "Variable for concordion:set must start"
                        + " with '#'\n (i.e. change concordion:set=\"" + expression + "\" to concordion:set=\"#" + expression + "\".");
            string rawVariableName = expression.Substring(1);
            return this.OgnlContext[rawVariableName];
        }

        public virtual void setVariable(string expression, object value)
        {
            Check.isTrue(expression.StartsWith("#"), "Variable for concordion:set must start"
                        + " with '#'\n (i.e. change concordion:set=\"" + expression + "\" to concordion:set=\"#" + expression + "\".");
            if (expression.Contains("="))
            {
                this.evaluate(expression);
            }
            else
            {
                var rawVariable = expression.Substring(1);
                this.PutVariable(rawVariable, value);
            }
        }

        public virtual object evaluate(string expression)
        {
            try
            {
                Check.notNull(this.Fixture, "Root object is null");
                Check.notNull(expression, "Expression to evaluate cannot be null");
                return Ognl.getValue(expression, this.OgnlContext, this.Fixture);
            }
            catch (OgnlException ognlException)
            {
                throw ognlException.getReason();
            }
        }

        #endregion
    }
}
