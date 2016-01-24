using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Concordion.NET.Internal
{
    public class SimpleEvaluator : OgnlEvaluator
    {
        #region Fields

        private static readonly string METHOD_NAME_PATTERN = "[a-z][a-zA-Z0-9_]*";
        private static readonly string PROPERTY_NAME_PATTERN = "[a-z][a-zA-Z0-9_]*";
        private static readonly string STRING_PATTERN = "'[^']+'";
        private static readonly string LHS_VARIABLE_PATTERN = "#" + METHOD_NAME_PATTERN;
        private static readonly string RHS_VARIABLE_PATTERN = "(" + LHS_VARIABLE_PATTERN + "|#TEXT|#HREF|#LEVEL)";

        #endregion

        #region Properties



        #endregion

        #region Constructor
        
        public SimpleEvaluator(object fixture)
            : base(fixture)
        {
        } 

        #endregion

        #region Methods

        private void ValidateEvaluationExpression(string expression)
        {
            string METHOD_CALL_PARAMS = METHOD_NAME_PATTERN + " *\\( *" + RHS_VARIABLE_PATTERN + "(, *" + RHS_VARIABLE_PATTERN + " *)*\\)";
            string METHOD_CALL_NO_PARAMS = METHOD_NAME_PATTERN + " *\\( *\\)";
            string TERNARY_STRING_RESULT = " \\? " + STRING_PATTERN + " : " + STRING_PATTERN;
            
            List<string> regexPatterns = new List<string>();
            regexPatterns.Add(PROPERTY_NAME_PATTERN);
            regexPatterns.Add(METHOD_CALL_NO_PARAMS);
            regexPatterns.Add(METHOD_CALL_PARAMS);
            regexPatterns.Add(RHS_VARIABLE_PATTERN);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + "(\\." + PROPERTY_NAME_PATTERN +  ")+");
            regexPatterns.Add(LHS_VARIABLE_PATTERN + " *= *" + PROPERTY_NAME_PATTERN);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + " *= *" + METHOD_CALL_NO_PARAMS);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + " *= *" + METHOD_CALL_PARAMS);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + TERNARY_STRING_RESULT);
            regexPatterns.Add(PROPERTY_NAME_PATTERN + TERNARY_STRING_RESULT);
            regexPatterns.Add(METHOD_CALL_NO_PARAMS + TERNARY_STRING_RESULT);
            regexPatterns.Add(METHOD_CALL_PARAMS + TERNARY_STRING_RESULT);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + "\\." + METHOD_CALL_NO_PARAMS);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + "\\." + METHOD_CALL_PARAMS);
            
            expression = expression.Trim();

            foreach (string regexPattern in regexPatterns) 
            {
                if (Regex.IsMatch(expression, regexPattern)) 
                {
                    return;
                }
            }

            throw new InvalidOperationException("Invalid expression [" + expression + "]");
        }

        private void ValidateSetVariableExpression(string expression)
        {
            List<string> regexPatterns = new List<string>();
            regexPatterns.Add(RHS_VARIABLE_PATTERN);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + "\\." + PROPERTY_NAME_PATTERN);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + " *= *" + PROPERTY_NAME_PATTERN);
            regexPatterns.Add(LHS_VARIABLE_PATTERN + " *= *" + METHOD_NAME_PATTERN + " *\\( *\\)");
            regexPatterns.Add(LHS_VARIABLE_PATTERN + " *= *" + METHOD_NAME_PATTERN + " *\\( *" + RHS_VARIABLE_PATTERN + "(, *" + RHS_VARIABLE_PATTERN + " *)*\\)");

            expression = expression.Trim();

            foreach (string regexPattern in regexPatterns)
            {
                if (Regex.IsMatch(expression, regexPattern))
                {
                    return;
                }
            }

            throw new InvalidOperationException("Invalid expression [" + expression + "]");
        }

        public static object ConvertToJavaTypes(object value)
        {
            if (value is Boolean) return new java.lang.Boolean((bool)value);
            if (value is int) return new java.lang.Integer((int)value);
            if (value is Int64) return new java.lang.Long((Int64)value);
            if (value is double) return new java.lang.Double((double)value);
            if (value is string) return java.lang.String.valueOf(value);
            if (value is IEnumerable)
            {
                var enumerable = value as IEnumerable;
                var iterable = new java.util.ArrayList();
                foreach (var resultItem in enumerable)
                {
                    iterable.Add(resultItem);
                }
                return iterable;
            }

            return value;
        }

        public static object ConvertToDotnetTypes(object value)
        {
            if (value is java.lang.Boolean) return Convert.ToBoolean(value.ToString());
            if (value is java.lang.Integer) return Convert.ToInt32(value.ToString());
            if (value is java.lang.Long) return Convert.ToInt64(value.ToString());
            if (value is java.lang.Double) return Convert.ToDouble(value.ToString());
            //if (value is ) return Convert.ToDateTime(value);
            if (value is java.lang.String) return Convert.ToString(value.ToString());
            return value;
        }

        #endregion

        #region IEvaluator Members

        public override object  evaluate(string expression)
        {
            this.ValidateEvaluationExpression(expression);
            var result = base.evaluate(expression);
            return ConvertToJavaTypes(result);
        }

        public override void setVariable(string expression, object value)
        {
            this.ValidateSetVariableExpression(expression);
            base.setVariable(expression, ConvertToDotnetTypes(value));
        }

        public override object getVariable(string expression)
        {
            var result = base.getVariable(expression);
            return ConvertToJavaTypes(result);
        }

        #endregion
    }
}
