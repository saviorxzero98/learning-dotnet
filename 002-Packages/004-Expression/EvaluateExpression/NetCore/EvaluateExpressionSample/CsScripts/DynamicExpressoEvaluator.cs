using DynamicExpresso;
using System;

namespace EvaluateExpressionSample.CsScripts
{
    /// <summary>
    /// https://github.com/davideicardi/DynamicExpresso/
    /// </summary>
    public class DynamicExpressoEvaluator
    {
        public static string Eval(string expression)
        {
            var interpreter = new Interpreter();

            return Convert.ToString(interpreter.Eval(expression));
        }
    }
}
