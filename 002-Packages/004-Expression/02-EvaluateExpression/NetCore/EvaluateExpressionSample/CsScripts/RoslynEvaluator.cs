using EvaluateExpressionSample.Scripts;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EvaluateExpressionSample.CsScripts
{
    /// <summary>
    /// https://github.com/dotnet/roslyn
    /// </summary>
    public static class RoslynEvaluator
    {
        public static ScriptOptions GetScriptOptions()
        {
            var imports = new List<string>
            {
                "System",
                "System.Math",
                "System.Linq",
                "System.Text",
                "System.Text.RegularExpressions",
                "System.Collections",
                "System.Collections.Generic",
                typeof(Json).Namespace
            };

            var references = new List<Assembly>
            {
                typeof(Match).Assembly,
                typeof(Json).Assembly,
                typeof(Regex).Assembly
            };

            return ScriptOptions.Default.WithReferences(references)
                                        .WithImports(imports)
                                        .WithAllowUnsafe(false);
        }


        public static object Eval(string expression)
        {
            var options = GetScriptOptions();
            var result = CSharpScript.EvaluateAsync(expression, options).GetAwaiter().GetResult();
            return Convert.ToString(result);
        }

        public async static Task<object> EvalAsync(string expression)
        {
            var options = GetScriptOptions();
            var result = await CSharpScript.EvaluateAsync(expression, options);
            return Convert.ToString(result);
        }
    }
}
