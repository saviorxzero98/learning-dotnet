using System.Text;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/nilproject/NiL.JS
    /// NiL.JS JavaScript 引擎
    /// 支援 ES5、ES6
    /// </summary>
    public static class NiLJSEvaluator
    {
        public static string Eval(string expression)
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append("function runExpression() {");
            codeBuilder.Append("JSON.create = function(obj) { return JSON.stringify(obj); };");
            codeBuilder.Append($"return {expression};");
            codeBuilder.Append("}");
            codeBuilder.Append("runExpression()");
            
            var context = new NiL.JS.Core.Context();
            return context.Eval(expression).ToString();
        }
    }
}
