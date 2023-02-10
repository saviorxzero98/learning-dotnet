using Newtonsoft.Json.Linq;
using NiL.JS.Core;
using NiLJSSample.Models;
using NiLJSSample.Samples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NiLJSSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //DemoEvalCode();
            DemoEvalCodeWithVariables();
            //DemoEvalCodeWithModule();
        }

        static void DemoEvalCode()
        {
            var start = StartDemo("Demo Eval Code");


            var expressions = new List<string>()
            {
                "new Date().toLocaleString()",
                "(5 * 5 * 3.14) + 100",
                "`${\"Hello\"} ${\"World\"}`",
                "JSON.stringify({\"id\": 1, \"name\": \"ace\" });"
            };

            foreach (var expression in expressions)
            {
                Console.WriteLine(EvalCodeSample.Eval(expression));
            }

            EndDemo(start);
        }

        static void DemoEvalCodeWithVariables()
        {
            var start = StartDemo("Demo Eval Code With Variables");


            var expressions = new List<string>()
            {
                "new Date().toLocaleString()",
                "(Raduis * Raduis * Pi) + 100",
                "`${\"Hello\"} ${World}`",
                //"JSON.stringify(Data.SubItem)",
                "JSON.stringify(Data.Items)",
                "JSON.stringify({\"id\": Data.Id, \"name\": Data.Name, \"item\": Data.Items.length });"
            };
            
            var data = new Item()
            {
                Id = 1, 
                Name = "ace", 
                Items = new string[] { "A", "B" } 
            };
            data.SubItem.Add(new Item() { Id = 11, Name = "jack" });
            data.SubItem.Add(new Item() { Id = 12, Name = "queen" });
            data.SubItem.Add(new Item() { Id = 13, Name = "king" });
            data.SubItem.Add(new Item() { Id = 14, Name = "joker" });
            
            var variables = new Dictionary<string, object>()
            {
                { "Raduis", 5 },
                { "Pi", Math.PI },
                { "World", "World" },
                //{ "Data", JSToken.Convert(JToken.FromObject(data)) }
                { "Data", data },
                { "MyArray", new string[] { "A", "B" }  }
            };

            foreach (var expression in expressions)
            {
                Console.WriteLine(EvalCodeSample.Eval(expression, variables));
            }

            EndDemo(start);
        }

        static void DemoEvalCodeWithModule()
        {
            var packages = new List<string>()
            {
                "modules\\lodash.js",
                "modules\\luxon.js"
            };
            packages = packages.Select(t => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, t))
                               .ToList();

            var code = new StringBuilder();
            code.Append("console.log(add(1, 2));");


            var eval = new EvalCodeWithModuleSample(packages);


            var start = StartDemo("Demo Eval Code With Module");

            var expressions = new List<string>()
            {
                "luxon.DateTime.local().toFormat('yyyy-MM-dd')",
                "_.take([1, 2, 3], 2)"
            };

            foreach (var expression in expressions)
            {
                Console.WriteLine(eval.Eval(expression));
            }

            EndDemo(start);
        }


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
