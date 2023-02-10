using EvaluateExpressionSample.Adaptives;
using EvaluateExpressionSample.JavaScripts;
using EvaluateExpressionSample.CsScripts;
using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluateExpressionSample.PyScripts;

namespace EvaluateExpressionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DemoAdaptiveExpression();

            DemoRoslyn();
            AsyncContext.Run(() => DemoRoslynAsync());

            DemoCodingSebExpressionEvaluator();

            DemoDateTable();

            DemoDynamicExpresso();

            DemoFlee();

            DemoMsieJsEngine(GetJsExpressions(isSupportES6: true));

            DemoJintJsEngine(GetJsExpressions(isSupportES6: true));

            DemoJurassicJsEngine(GetJsExpressions(isSupportES6: true));

            DemoNiLJsEngine(GetJsExpressions(isSupportES6: true));

            DemoVroomJsEngine(GetJsExpressions(isSupportES6: true));

            DemoClearScriptEngine(GetJsExpressions(isSupportES6: true));

            DemoIronPythonEvaluator();
        }

        static List<string> GetJsExpressions(bool isSupportES6)
        {
            var expressions = new List<string>()
            {
                "new Date().toLocaleString()",
                "(5 * 5 * 3.14) + 100",
                (isSupportES6) ? "`${\"Hello\"} ${\"World\"}`": "\"Hello\" + \" \" + \"World\"",
                "JSON.stringify({\"id\": 1, \"name\": \"ace\" });"
            };
            return expressions;
        }


        #region DataTable Expression

        // Demo DataTable
        static void DemoDateTable()
        {
            var start = StartDemo("Date Table");

            var expression1 = $"(5 * 5 * {Math.PI}) + 100";

            Console.WriteLine(DataTableEvaluator.Eval(expression1));

            EndDemo(start);
        }

        #endregion


        #region C# Expression

        // Demo Roslyn C# Script
        static void DemoRoslyn()
        {
            var start = StartDemo("Roslyn C# Script");

            var expression1 = "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")";
            var expression2 = "(5 * 5 * PI) + 100";
            var expression3 = "\"Hello\" + \" \" + \"world!\"";
            var expression4 = "(DateTime.Now - DateTime.Today).TotalHours + \" hrs\"";
            var expression5 = "Json.Create(new { Id = 1, Name = \"Ace\" })";
            var expression6 = "Json.Create(new int[] { 1, 2, 3, 4, 5, })";
            var expression7 = "new List<string>(){ \"Test\", \"Hello\", \"Bye\", \"How are you?\" }.Find(t => t.Length < 4)";
            var expression8 = "\"testsplit\".Split('s')[0]";

            Console.WriteLine(RoslynEvaluator.Eval(expression1));
            Console.WriteLine(RoslynEvaluator.Eval(expression2));
            Console.WriteLine(RoslynEvaluator.Eval(expression3));
            Console.WriteLine(RoslynEvaluator.Eval(expression4));
            Console.WriteLine(RoslynEvaluator.Eval(expression5));
            Console.WriteLine(RoslynEvaluator.Eval(expression6));
            Console.WriteLine(RoslynEvaluator.Eval(expression7));
            Console.WriteLine(RoslynEvaluator.Eval(expression8));

            EndDemo(start);
        }

        // Demo Roslyn C# Script
        static async Task DemoRoslynAsync()
        {
            var start = StartDemo("Roslyn C# Script");

            var expression1 = "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")";
            var expression2 = "(5 * 5 * PI) + 100";
            var expression3 = "\"Hello\" + \" \" + \"world!\"";
            var expression4 = "(DateTime.Now - DateTime.Today).TotalHours + \" hrs\"";
            var expression5 = "Json.Create(new { Id = 1, Name = \"Ace\" })";
            var expression6 = "Json.Create(new int[] { 1, 2, 3, 4, 5, })";
            var expression7 = "new List<string>(){ \"Test\", \"Hello\", \"Bye\", \"How are you?\" }.Find(t => t.Length < 4)";
            var expression8 = "\"testsplit\".ToPascalCase()";

            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression1));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression2));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression3));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression4));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression5));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression6));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression7));
            Console.WriteLine(await RoslynEvaluator.EvalAsync(expression8));

            EndDemo(start);
        }

        // Demo CodingSeb ExpressionEvaluator
        static void DemoCodingSebExpressionEvaluator()
        {
            var start = StartDemo("CodingSeb Expression");
            var expression1 = "Date.Today(5, 30, 20).ToString(\"yyyy-MM-dd HH:mm:ss\")";
            var expression2 = "(5 * 5 * Math.PI) + 100";
            var expression3 = "\"Hello\" + \" \" + \"world!\"";
            var expression4 = "(DateTime.Now - DateTime.Today).TotalHours + \"hrs\"";
            var expression5 = "Json.Create(new { Id = 1, Name = \"Ace\" })";
            var expression6 = "Json.Create(new int[] { 1, 2, 3, 4, 5, })";
            var expression7 = "new List<string>(){ \"Test\", \"Hello\", \"Bye\", \"How are you?\" }.Find(t => t.Length < 4)";
            var expression8 = "(DateTime.Now.Hour <= 12) ? \"AM\":\"PM\";";
            var expression9 = "\"testsplit\".Split('s', StringSplitOptions.None)[0]";

            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression1));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression2));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression3));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression4));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression5));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression6));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression7));
            Console.WriteLine(CodingSebExpressionEvaluator.RunScript(expression8));
            Console.WriteLine(CodingSebExpressionEvaluator.Eval(expression9));


            EndDemo(start);
        }

        // Demo Dynamic Expresso
        static void DemoDynamicExpresso()
        {
            var start = StartDemo("Dynamic Expresso");

            var expression1 = "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")";
            var expression2 = "(5 * 5 * Math.PI) + 100";
            var expression3 = "\"Hello\" + \" \" + \"world!\"";
            var expression4 = "(DateTime.Now - DateTime.Today).TotalHours + \" hrs\"";

            Console.WriteLine(DynamicExpressoEvaluator.Eval(expression1));
            Console.WriteLine(DynamicExpressoEvaluator.Eval(expression2));
            Console.WriteLine(DynamicExpressoEvaluator.Eval(expression3));
            Console.WriteLine(DynamicExpressoEvaluator.Eval(expression4));

            EndDemo(start);
        }


        // Demo Flee Expresso
        static void DemoFlee()
        {
            var start = StartDemo("Flee");

            var expression1 = "DateTime.Now.ToString(\"yyyy-MM-dd HH:mm:ss\")";
            var expression2 = "(5 * 5 * Math.PI) + 100";
            var expression3 = "\"Hello\" + \" \" + \"world!\"";
            var expression4 = "(DateTime.Now - DateTime.Today).TotalHours + \"hrs\"";

            Console.WriteLine(FleeEvaluator.Eval(expression1));
            Console.WriteLine(FleeEvaluator.Eval(expression2));
            Console.WriteLine(FleeEvaluator.Eval(expression3));
            Console.WriteLine(FleeEvaluator.Eval(expression4));

            EndDemo(start);
        }

        #endregion


        #region Adaptive Expression

        // Demo Adaptive Expressions
        static void DemoAdaptiveExpression()
        {
            var start = StartDemo("Adaptive Expressions");

            var expression1 = "addHours(utcNow(), 8, 'yyyy-MM-dd HH:mm:ss')";
            var expression2 = "(5 * 5 * PI) + 100";
            var data2 = new { PI = Math.PI };
            var expression3 = "split(Text, ' ')";
            var data3 = new { Text = "Hello World!" };
            var expression4 = "first(select(split(Text, ','), s, toUpper(s)))";
            var expression5 = "first(where(split(Text, ','), s, startsWith(s, 'C')))";
            var data4 = new { Text = "Apple,Banana,Cherry,Durian" };
            var expression6 = "json('{\"ID\": \"888\"}')";

            var expression7 = "concat(Name, ' (', Detial.Description, ')')";
            var data7 = new UserProfile()
            {
                Name = "張三",
                Detial = new Detiall()
                {
                    Description = "弓長張一一一",
                    CreateDate = DateTime.Now
                }
            };

            var expression8 = "localNow('yyyy-MM-dd HH:mm:ss')";

            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression1));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression2, data2));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression3, data3));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression4, data4));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression5, data4));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression6));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression7, data7));
            Console.WriteLine(AdaptiveExpressionEvaluator.Eval(expression8));

            EndDemo(start);
        }

        #endregion


        #region JavaScript Expression

        // Demo Mise Evaluate
        static void DemoMsieJsEngine(List<string> expressions)
        {
            var start = StartDemo("Mise Evaluate");

            foreach (var expression in expressions)
            {
                Console.WriteLine(MsieEvaluator.Eval(expression));
            }

            EndDemo(start);
        }

        // Demo Jint Evaluate
        static void DemoJintJsEngine(List<string> expressions)
        {
            var start = StartDemo("Jint Evaluate");

            foreach (var expression in expressions)
            {
                Console.WriteLine(JintEvaluator.Eval(expression));
            }

            EndDemo(start);
        }

        // Demo Jurassic Evaluate
        static void DemoJurassicJsEngine(List<string> expressions)
        {
            var start = StartDemo("Jurassic Evaluate");

            foreach (var expression in expressions)
            {
                Console.WriteLine(JurassicEvaluator.Eval(expression));
            }

            EndDemo(start);
        }

        // Demo NiL.JS Evaluate
        static void DemoNiLJsEngine(List<string> expressions)
        {
            var start = StartDemo("NiL.JS Evaluate");

            foreach (var expression in expressions)
            {
                Console.WriteLine(NiLJSEvaluator.Eval(expression));
            }

            EndDemo(start);
        }

        // Demo VroomJs Evaluate
        static void DemoVroomJsEngine(List<string> expressions)
        {
            var start = StartDemo("VroomJs Evaluate");

            foreach (var expression in expressions)
            {
                Console.WriteLine(NiLJSEvaluator.Eval(expression));
            }

            EndDemo(start);
        }

        // Demo Clear Script
        static void DemoClearScriptEngine(List<string> expressions)
        {
            var start = StartDemo("ClearScript Evaluate");

            foreach (var expression in expressions)
            {
                Console.WriteLine(ClearScriptEvaluator.Eval(expression));
            }

            EndDemo(start);
        }

        #endregion


        #region Python Expression

        static void DemoIronPythonEvaluator()
        {
            var start = StartDemo("Iron Python");

            var expression1 = $"(5 * 5 * math.pi) + 100";
            var expression2 = $"datetime.datetime.now().strftime(\"%Y-%m-%d %H:%M:%S\")";
            
            Console.WriteLine(IronPythonEvaluator.Eval(expression1));
            Console.WriteLine(IronPythonEvaluator.Eval(expression2));

            EndDemo(start);
        }

        #endregion


        static DateTime StartDemo(string demoName)
        {
            DateTime start = DateTime.Now;

            Console.WriteLine($"===== Demo {demoName} =====");


            return start;
        }

        static void EndDemo(DateTime start)
        {
           
            DateTime end = DateTime.Now;

            Console.WriteLine($"Execute Time: { (end - start).TotalMilliseconds } ms\n\n");
        }
    }
}
