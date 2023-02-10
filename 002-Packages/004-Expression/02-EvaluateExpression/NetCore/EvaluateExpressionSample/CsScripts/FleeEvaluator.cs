using Flee.PublicTypes;
using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace EvaluateExpressionSample.CsScripts
{
    /// <summary>
    /// https://github.com/mparlak/Flee
    /// </summary>
    public static class FleeEvaluator
    {
        public static string Eval(string expression)
        {
            var context = new ExpressionContext();

            context.Imports.AddType(typeof(Math), "Math");
            context.Imports.AddType(typeof(DateTime), "DateTime");
            context.Imports.AddType(typeof(TimeSpan), "TimeSpan");
            context.Imports.AddType(typeof(Regex), "Regex");
            context.Imports.AddType(typeof(RegexOptions), "RegexOptions");
            context.Imports.AddType(typeof(IEnumerable), "List");

            var generic = context.CompileGeneric<object>(expression);
            return Convert.ToString(generic.Evaluate());
        }
    }
}
