using System.Text;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/sebastienros/jint
    /// Jint JavaScript 引擎
    /// 只支援至 ES5，不支援到 ES6
    /// </summary>
    public static class JintEvaluator
    {
        public static string Eval(string expression)
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append("function runExpression() {");
            codeBuilder.Append("JSON.create = function(obj) { return JSON.stringify(obj); };");
            codeBuilder.Append($"return {expression};");
            codeBuilder.Append("}");
            

            var engine = new Jint.Engine();
            engine.Execute(codeBuilder.ToString());
            
            return engine.Invoke("runExpression").ToString();
        }
    }
}
