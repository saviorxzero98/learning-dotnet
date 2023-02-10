using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingsebExpressionEvaluatorSample.Samples
{
    public static class ScriptSample
    {
        public static void Demo()
        {
            var start = StartDemo("Demo Script");

            var script1 = "if (Age <= 18)\n{\nreturn \"未成年\"; \n}\nelse \n{\n return \"已成年\";\n}";
            var variables1a = new Dictionary<string, object>()
            {
                { "Age", 15 }
            };
            var variables1b = new Dictionary<string, object>()
            {
                { "Age", 21 }
            };

            var script2 = "List<int> matchedAges = new List<int>();\nfor(int i=0; i<Ages.Count;i++)\n{\nif (Ages[i] <= 18)\n{\nmatchedAges.Add(Ages[i]);\n}\n}\nreturn JToken.FromObject(matchedAges);";
            var variables2 = new Dictionary<string, object>()
            {
                { "Ages", JToken.FromObject(new List<int>() { 6, 3, 24, 17, 30, 92 }) }
            };

            var script3 = "var filterData = Data.Where(d => d.Age < 18).Select(d => d.Name);\nreturn JToken.FromObject(filterData);";
            var variables3 = new Dictionary<string, object>()
            {
                { "Data", new List<UserProfile>()
                    {
                        new UserProfile() { Name = "Ace", Age = 10, CreateDate = "2020-03-03"  },
                        new UserProfile() { Name = "Jack", Age = 11, CreateDate = "2020-04-04" },
                        new UserProfile() { Name = "King", Age = 70, CreateDate = "2020-08-08" },
                        new UserProfile() { Name = "Joke", Age = 24, CreateDate = "2020-11-11" }
                    }
                }
            };

            var data = new List<UserProfile>()
                    {
                        new UserProfile() { Name = "Ace", Age = 10, CreateDate = "2020-03-03"  },
                        new UserProfile() { Name = "Jack", Age = 11, CreateDate = "2020-04-04" },
                        new UserProfile() { Name = "King", Age = 70, CreateDate = "2020-08-08" },
                        new UserProfile() { Name = "Joke", Age = 24, CreateDate = "2020-11-11" }
                    };
            data.Where(d => d.Age < 18).Select(d => d.Name).ToArray();

            var evaluator = new CodingSebExpressionEvaluator();
            Console.WriteLine(evaluator.RunScript(script1, variables1a));
            Console.WriteLine(evaluator.RunScript(script1, variables1b));
            Console.WriteLine(evaluator.RunScript(script2, variables2));

            var result = evaluator.RunScript<List<string>>(script3, variables3);
            Console.WriteLine(evaluator.RunScript(script3, variables3));
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

    public class UserProfile
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string CreateDate { get; set; }
    }
}
