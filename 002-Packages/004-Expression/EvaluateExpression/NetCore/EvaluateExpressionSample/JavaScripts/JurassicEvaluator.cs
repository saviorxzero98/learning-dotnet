using System.Text;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/paulbartrum/jurassic
    /// Jurassic JavaScript 引擎
    /// 支援 ES5，和支援部分的 ES6
    /// </summary>
    public static class JurassicEvaluator
    {
        public static string Eval(string expression)
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append("function runExpression() {");
            codeBuilder.Append("JSON.create = function(obj) { return JSON.stringify(obj); };");
            codeBuilder.Append($"return {expression};");
            codeBuilder.Append("}");


            var engine = new Jurassic.ScriptEngine();
            engine.Execute(codeBuilder.ToString());
            return engine.Evaluate("runExpression()").ToString();
        }
    }
}
