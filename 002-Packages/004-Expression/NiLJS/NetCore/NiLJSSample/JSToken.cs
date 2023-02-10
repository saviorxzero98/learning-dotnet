using Newtonsoft.Json.Linq;
using NiL.JS.Core;
using System.Collections.Generic;
using System.Linq;

namespace NiLJSSample
{
    public class JSToken : JSObject
    {
        public static JSValue Convert(JToken token)
        {
            JTokenType type = token.Type;

            switch (type)
            {
                case JTokenType.Object:
                    return ToJSObject((JObject)token);
                case JTokenType.Array:
                    return ToJSObject((JArray)token);
                case JTokenType.Boolean:
                    return JSValue.Marshal(token.ToObject<bool>());
                case JTokenType.Float:
                    return JSValue.Marshal(token.ToObject<double>());
                case JTokenType.Integer:
                    return JSValue.Marshal(token.ToObject<int>());
                case JTokenType.String:
                    return JSValue.Marshal(token.ToString());
                case JTokenType.Date:
                case JTokenType.TimeSpan:
                    return JSValue.Marshal(token.ToString());
                default:
                    return JSValue.Null;
            }
        }

        internal static JSValue ToJSObject(JObject jObject)
        {
            if (jObject == null || !jObject.Properties().Any())
            {
                return JSValue.Null;
            }

            JSObject jsObject = JSObject.CreateObject();

            foreach (var property in jObject)
            {
                string key = property.Key;
                JToken value = property.Value;

                switch (value.Type)
                {
                    case JTokenType.Object:
                        jsObject.DefineProperty(key).Assign(ToJSObject((JObject)value));
                        break;
                    case JTokenType.Array:
                        jsObject.DefineProperty(key).Assign(ToJSObject((JArray)value));
                        break;
                    case JTokenType.Boolean:
                    case JTokenType.Float:
                    case JTokenType.Integer:
                    case JTokenType.String:
                        jsObject.DefineProperty(key).Assign(Convert(value));
                        break;
                    case JTokenType.Date:
                    case JTokenType.TimeSpan:
                        jsObject.DefineProperty(key).Assign(Convert(value.ToString()));
                        break;
                    default:
                        jsObject.DefineProperty(key).Assign(JSValue.Null);
                        break;
                }
            }

            return jsObject;
        }

        internal static JSValue ToJSObject(JArray jArray)
        {
            List<JSValue> array = new List<JSValue>();

            if (jArray == null || !jArray.HasValues)
            {
                return JSValue.Marshal(array);
            }

            var arrayType = jArray.FirstOrDefault();

            switch (arrayType.Type)
            {
                case JTokenType.Object:
                    List<JSValue> objectArray = jArray.Select(j => ToJSObject((JObject)j)).ToList();
                    return JSValue.Marshal(objectArray.ToArray());
                
                case JTokenType.Array:
                    List<JSValue> mutilArray = jArray.Select(j => ToJSObject((JArray)j)).ToList();
                    return JSValue.Marshal(mutilArray);
                
                case JTokenType.Boolean:
                    return JSValue.Marshal(ToArray<bool>(jArray));
                
                case JTokenType.Float:
                    return JSValue.Marshal(ToArray<double>(jArray));
                
                case JTokenType.Integer:
                    return JSValue.Marshal(ToArray<int>(jArray));
                
                case JTokenType.String:
                case JTokenType.Date:
                case JTokenType.TimeSpan:
                    List<string> datetimeArray = jArray.Select(j => j.ToString()).ToList();
                    return JSValue.Marshal(ToArray<string>(jArray));
            }

            return JSValue.Marshal(array);
        }

        private static List<T> ToArray<T>(JArray jArray)
        {
            List<T> array = jArray.Select(j => j.ToObject<T>()).ToList();
            return array;
        }
    }
}
