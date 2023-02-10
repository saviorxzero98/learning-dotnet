using System;

namespace CodingsebExpressionEvaluatorSample.Samples
{
    public static class ExpressionSample
    {
        public static void Demo()
        {
            var start = StartDemo("Demo Expression");

            var expression1 = "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")";
            var expression2 = "(5 * 5 * Math.PI) + 100";
            var expression3 = "\"Hello\" + \" \" + \"world!\"";
            var expression4 = "(DateTime.Now - DateTime.Today).TotalHours + \"hrs\"";
            var expression5 = "JToken.FromObject(new { Id = 1, Name = \"Ace\" })";
            var expression6 = "JToken.FromObject(new int[] { 1, 2, 3, 4, 5, })";
            var expression7 = "new List<string>(){ \"Test\", \"Hello\", \"Bye\", \"How are you?\" }.Find(t => t.Length < 4)";
            var expression8 = "(DateTime.Now.Hour <= 12) ? \"AM\":\"PM\";";
            var expression9 = "\"testsplit\".Split('s', StringSplitOptions.None)[0]";

            var evaluator = new CodingSebExpressionEvaluator();
            Console.WriteLine(evaluator.Eval(expression1));
            Console.WriteLine(evaluator.Eval(expression2));
            Console.WriteLine(evaluator.Eval(expression3));
            Console.WriteLine(evaluator.Eval(expression4));
            Console.WriteLine(evaluator.Eval(expression5));
            Console.WriteLine(evaluator.Eval(expression6));
            Console.WriteLine(evaluator.Eval(expression7));
            Console.WriteLine(evaluator.RunScript(expression8));
            Console.WriteLine(evaluator.Eval(expression9));


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
