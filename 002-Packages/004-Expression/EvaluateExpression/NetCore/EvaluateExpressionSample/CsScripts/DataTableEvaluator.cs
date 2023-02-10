using System;
using System.Data;

namespace EvaluateExpressionSample.CsScripts
{
    /// <summary>
    /// DataTable.Compute
    /// 僅支援算術運算
    /// </summary>
    public static class DataTableEvaluator
    {
        public static string Eval(string expression)
        {
            return Convert.ToString(new DataTable().Compute(expression, string.Empty));
        }
    }
}
