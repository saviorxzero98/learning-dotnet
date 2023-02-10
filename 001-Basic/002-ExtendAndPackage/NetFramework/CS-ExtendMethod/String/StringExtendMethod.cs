using System;

namespace ExtendMethod.String
{
    public static class StringExtendMethod
    {
        public static string ToHello(this string value)
        {
            return "Hello " + value + "!";
        }

        public static string ToHello(this string value, string name)
        {
            return "Hello " + name + ", I'm " + value + ".";
        }
    }
}
