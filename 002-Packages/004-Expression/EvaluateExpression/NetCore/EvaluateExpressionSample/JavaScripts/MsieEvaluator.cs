using MsieJavaScriptEngine;
using MsieJavaScriptEngine.Helpers;
using System;
using System.Text;

namespace EvaluateExpressionSample.JavaScripts
{
    /// <summary>
    /// https://github.com/Taritsyn/MsieJavaScriptEngine
    /// Chakra 引擎
    /// 支援 ES5，支援部分 ES6
    /// </summary>
    public class MsieEvaluator
    {
        public static string Eval(string expression)
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append("function runExpression() {");
            codeBuilder.Append("JSON.create = function(obj) { return JSON.stringify(obj); };");
            codeBuilder.Append($"return {expression};");
            codeBuilder.Append("}");
            

            try
            {
                using (var jsEngine = new MsieJsEngine())
                {
                    jsEngine.Execute(codeBuilder.ToString());
                    return jsEngine.CallFunction<string>("runExpression");
                }
            }
            catch (JsEngineLoadException e)
            {
                Console.WriteLine("During loading of JavaScript engine an error occurred.");
                Console.WriteLine();
                Console.WriteLine(JsErrorHelpers.GenerateErrorDetails(e));
            }
            catch (JsScriptException e)
            {
                Console.WriteLine("During processing of JavaScript code an error occurred.");
                Console.WriteLine();
                Console.WriteLine(JsErrorHelpers.GenerateErrorDetails(e));
            }
            catch (JsException e)
            {
                Console.WriteLine("During working of JavaScript engine an unknown error occurred.");
                Console.WriteLine();
                Console.WriteLine(JsErrorHelpers.GenerateErrorDetails(e));
            }
            return string.Empty;
        }
    }
}
