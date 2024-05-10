using YantraJS.Core;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/yantrajs/yantra
    /// </summary>
    public static class YantraJSEvaluator
    {
        public static string Eval(string expression)
        {
            var context = new JSContext();
            return context.Eval(expression).ToString();
        }
    }
}
