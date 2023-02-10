using ExtendMethod.String;
using System;

namespace ExtendMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Sample";
            Console.WriteLine(text.ToHello());

            Console.WriteLine(text.ToHello("Customer"));

            Console.ReadLine();
        }
    }
}
