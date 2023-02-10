using Newtonsoft.Json.Linq;
using NiL.JS.Core;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NiLJSSample.Samples
{
    public class EvalCodeWithModuleSample
    {
        protected List<string> Sources = new List<string>();

        public EvalCodeWithModuleSample(List<string> packages)
        {
            foreach (var package in packages)
            {
                string source = File.ReadAllText(package);
                Sources.Add(source);
            }
        }

        public object Eval(string expression)
        {
            var context = new Context();

            StringBuilder script = new StringBuilder();
            foreach (var source in Sources)
            {
                script.Append(source);
            }
            script.Append(expression);

            return context.Eval(script.ToString());
        }

        public object Eval(string expression, Dictionary<string, object> variables)
        {
            var context = new Context();

            var variableNames = variables.Keys;

            foreach (var name in variableNames)
            {
                var value = variables[name];
                var jsValue = ToJsValue(value);

                if (jsValue != JSValue.Null)
                {
                    context.DefineVariable(name).Assign(jsValue);
                }
            }

            StringBuilder script = new StringBuilder();
            foreach (var source in Sources)
            {
                script.Append(source);
            }
            script.Append(expression);

            return context.Eval(expression);
        }

        private static JSValue ToJsValue(object value)
        {
            JToken jValue = JToken.FromObject(value);

            switch (jValue.Type)
            {
                case JTokenType.Boolean:
                case JTokenType.Float:
                case JTokenType.Integer:
                case JTokenType.String:
                    return JSValue.Marshal(value);
                case JTokenType.Date:
                case JTokenType.TimeSpan:
                    return JSValue.Marshal(value.ToString());
                case JTokenType.Object:
                case JTokenType.Array:
                    return JSValue.Wrap(value);
                default:
                    return JSValue.Null;
            }
        }
    }
}
