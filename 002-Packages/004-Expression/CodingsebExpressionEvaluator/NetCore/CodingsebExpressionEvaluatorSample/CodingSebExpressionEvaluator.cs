using CodingSeb.ExpressionEvaluator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace CodingsebExpressionEvaluatorSample
{
    public class CodingSebExpressionEvaluator
    {
        protected ExpressionEvaluator GetEvaluator()
        {
            var evaluator = new ExpressionEvaluator();

            var namespaces = new List<string>()
            {
                // Default
                "System",
                "System.Math",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.ComponentModel",
                "System.Collections",
                "System.Collections.Generic",
                "System.Collections.Specialized",
                "System.Globalization",
                "System.Security.Cryptography",

                // Common Package
                typeof(JToken).Namespace,
                typeof(JsonConvert).Namespace
            };

            

            // Reset Namespace
            evaluator.Namespaces = namespaces.Distinct().ToList();

            // Add Static Extension

            return evaluator;
        }

        public object Eval(string expression)
        {
            ExpressionEvaluator evaluator = GetEvaluator();
            object result = evaluator.Evaluate(expression);
            return result;
        }

        public object Eval(string expression, Dictionary<string, object> variables)
        {
            ExpressionEvaluator evaluator = GetEvaluator();

            if (variables != null)
            {
                evaluator.Variables = variables;
            }

            object result = evaluator.Evaluate(expression);
            return result;
        }

        public object RunScript(string script)
        {
            ExpressionEvaluator evaluator = GetEvaluator();
            
            object result = evaluator.ScriptEvaluate(script);
            return result;
        }

        public object RunScript(string script, Dictionary<string, object> variables)
        {
            ExpressionEvaluator evaluator = GetEvaluator();


            if (variables != null)
            {
                evaluator.Variables = variables;
            }

            object result = evaluator.ScriptEvaluate(script);
            return result;
        }

        public T RunScript<T>(string script, Dictionary<string, object> variables)
        {
            ExpressionEvaluator evaluator = GetEvaluator();


            if (variables != null)
            {
                evaluator.Variables = variables;
            }

            T result = evaluator.ScriptEvaluate<T>(script);
            return result;
        }
    }
}
