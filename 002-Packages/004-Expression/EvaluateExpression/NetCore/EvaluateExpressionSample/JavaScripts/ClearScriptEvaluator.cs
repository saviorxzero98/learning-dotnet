using Microsoft.ClearScript.V8;
using Newtonsoft.Json.Linq;
using System.Text;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/Microsoft/ClearScript
    /// Clear Script JavaScript 引擎
    /// 
    /// </summary>
    public class ClearScriptEvaluator
    {
        public static string Eval(string expression)
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append("function runExpression() {");
            //codeBuilder.Append("JSON.create = function(obj) { return JSON.stringify(obj); };");
            codeBuilder.Append($"return {expression};");
            codeBuilder.Append("}");
            codeBuilder.Append("results = runExpression();");

            using (var engine = new V8ScriptEngine()) 
            {
                engine.Execute(codeBuilder.ToString());
                var values = JToken.FromObject(engine.Script.results);
                return values.ToString();
            }
        }
    }
}
