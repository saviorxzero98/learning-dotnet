using Newtonsoft.Json.Linq;
using NiL.JS.Core;
using NiL.JS.Extensions;
using System.Collections.Generic;

namespace NiLJSSample.Samples
{
    public static class EvalCodeSample
    {
        public static object Eval(string expression)
        {
            var context = new Context();

            return context.Eval(expression);
        }

        public static object Eval(string expression, Dictionary<string, object> variables)
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

            string code = @"
function toArray(objArray) {
    let array = [];  
    if (objArray) {
        for(let i = 0; i < objArray.length; i++) {
            array.push(objArray[i]);
        }   
    }
    return array;
}
";

            return context.Eval(code + expression);
        }

        private static JSValue ToJsValue(object value)
        {
            if (value is JSObject)
            {
                return (JSObject) value;
            }

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
                    return JSValue.Marshal(value);
                case JTokenType.Array:
                    JSValue array = JSValue.Marshal(value);
                    bool isArray = array.IsIterable();
                    return array;
                default:
                    return JSValue.Null;
            }
        }
    }
}
