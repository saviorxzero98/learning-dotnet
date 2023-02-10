using System.Text;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/pauldotknopf/vroomjs-core
    /// VroomJs JavaScript 引擎
    /// 支援 ES5、ES6
    /// </summary>
    public static class VroomJsEvaluator
    {
        public static string Eval(string expression)
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append("function runExpression() {");
            codeBuilder.Append("JSON.create = function(obj) { return JSON.stringify(obj); };");
            codeBuilder.Append($"return {expression};");
            codeBuilder.Append("}");
            codeBuilder.Append("runExpression()");

            using (var engine = new VroomJs.JsEngine())
            {
                using (var context = engine.CreateContext())
                {
                    return context.Execute(expression).ToString();
                }
            }
        }
    }
}
