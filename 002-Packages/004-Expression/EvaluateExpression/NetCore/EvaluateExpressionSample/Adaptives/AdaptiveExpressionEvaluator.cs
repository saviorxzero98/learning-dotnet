using AdaptiveExpressions;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;

namespace EvaluateExpressionSample.Adaptives
{
    public class AdaptiveExpressionEvaluator
    {

        public static void AddCustomFunction()
        {
            // 清除 Functions
            Expression.Functions.Clear();

            // 加入自訂 Function
            Expression.Functions.Add("localNow", (args) =>
            {
                var now = DateTime.Now;
                string isoFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

                if (args != null && args.Count > 0 && args[0] != null)
                {
                    isoFormat = Convert.ToString(args[0]);
                }
                
                return now.ToString(isoFormat);
            });
        }

        public static JToken Eval(string expression, object data = null)
        {
            try
            {
                // Parse Expression
                var exp = Expression.Parse(expression);

                // Add Custom Function
                AddCustomFunction();

                // Eval Expression
                var (value, error) = exp.TryEvaluate(data);

                if (error == null && value != null)
                {
                    var result = JToken.FromObject(value);
                    return result;
                }
                return JToken.FromObject(expression);
            }
            catch
            {
                return expression;
            }
        }
    }
}
