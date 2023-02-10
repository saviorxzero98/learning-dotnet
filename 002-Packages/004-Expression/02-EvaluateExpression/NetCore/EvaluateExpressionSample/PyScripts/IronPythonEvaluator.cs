using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System;

namespace EvaluateExpressionSample.PyScripts
{
    /// <summary>
    /// https://github.com/IronLanguages/ironpython2
    /// </summary>
    public class IronPythonEvaluator
    {
        public static string Eval(string expression)
        {
            ScriptEngine pythonEngine = Python.CreateEngine();
            ScriptScope pythonScope = pythonEngine.CreateScope();

            pythonScope.ImportModule("datetime");
            pythonScope.ImportModule("time");
            pythonScope.ImportModule("math");

            ScriptSource pythonScriptSource = pythonEngine.CreateScriptSourceFromString(expression, SourceCodeKind.Expression);

            dynamic result = pythonScriptSource.Execute(pythonScope);

            return Convert.ToString(result);
        }
    }
}
