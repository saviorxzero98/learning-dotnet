using CodingSeb.ExpressionEvaluator;
using EvaluateExpressionSample.Scripts;
using System;
using System.Collections.Generic;

namespace EvaluateExpressionSample.CsScripts
{
    /// <summary>
    /// https://github.com/codingseb/ExpressionEvaluator
    /// </summary>
    public static class CodingSebExpressionEvaluator
    {
        public static string Eval(string expression)
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

                // Pre-build Functions
                typeof(Json).Namespace
            };

            evaluator.Namespaces = namespaces;

            // Add Static Extension
            evaluator.StaticTypesForExtensionsMethods.Add(typeof(StringEx));

            var result = evaluator.Evaluate(expression);

            return Convert.ToString(result);
        }

        public static string RunScript(string script)
        {
            var evaluator = new ExpressionEvaluator();
            evaluator.Namespaces.Add(typeof(Json).Namespace);

            var result = evaluator.ScriptEvaluate(script);

            return Convert.ToString(result);
        }
    }
}
