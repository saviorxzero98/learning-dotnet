using System;
using System.Collections.Generic;

namespace CodingsebExpressionEvaluatorSample.Samples
{
    public class ExpressionWithVariableSample
    {
        public static void Demo()
        {
            var start = StartDemo("Demo Expression Witch Variables");

            var expression1 = "DateTime.Now.ToString(DateFormat)";
            var variables1 = new Dictionary<string, object>()
            {
                { "DateFormat", "yyyy-MM-dd HH:mm:ss" }
            };

            var expression2 = "(Radius * Radius * Math.PI) + Delta";
            var variables2 = new Dictionary<string, object>()
            {
                { "Radius", 9.2 },
                { "Delta", 100 }
            };


            var evaluator = new CodingSebExpressionEvaluator();
            Console.WriteLine(evaluator.Eval(expression1, variables1));
            Console.WriteLine(evaluator.Eval(expression2, variables2));


            EndDemo(start);
        }

        private static DateTime StartDemo(string demoName)
        {
            DateTime start = DateTime.Now;

            Console.WriteLine($"===== Demo {demoName} =====");


            return start;
        }

        private static void EndDemo(DateTime start)
        {

            DateTime end = DateTime.Now;

            Console.WriteLine($"Execute Time: { (end - start).TotalMilliseconds } ms\n\n");
        }
    }
}
