using System;
using System.Collections;
using System.Collections.Generic;

namespace Concordion.NET.Internal
{
    public class BridgingEvaluator : OgnlEvaluator
    {
        #region Constructor
        
        public BridgingEvaluator(object fixture)
            : base(fixture)
        { } 

        #endregion

        #region Methods

        public static object ConvertToJavaTypes(object value)
        {
            if (value is Boolean) return new java.lang.Boolean((bool)value);
            if (value is int) return new java.lang.Integer((int)value);
            if (value is Int64) return new java.lang.Long((Int64)value);
            if (value is double) return new java.lang.Double((double)value);
            if (value is DateTime)
            {
                var datetime = (DateTime) value;
                return new java.util.Date(
                    datetime.Year, datetime.Month, datetime.Day,
                    datetime.Hour, datetime.Minute, datetime.Second);
            }
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
            if (value is java.util.Date) return Convert.ToDateTime(value);
            if (value is java.lang.String) return Convert.ToString(value.ToString());
            return value;
        }

        #endregion

        #region IEvaluator Members

        public override object  evaluate(string expression)
        {
            var result = base.evaluate(expression);
            return ConvertToJavaTypes(result);
        }

        public override void setVariable(string expression, object value)
        {
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
