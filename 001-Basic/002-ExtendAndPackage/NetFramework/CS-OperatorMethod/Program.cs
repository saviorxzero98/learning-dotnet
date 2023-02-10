using System;

namespace OperatorMethod
{
    class Program
    {
        const string Number1 = "123456789";
        const string Number2 = "987684321";

        static void Main(string[] args)
        {
            Console.WriteLine("===== Before =====");
            LicensePlateB beforeA = new LicensePlateB(Number1);
            LicensePlateB beforeB = new LicensePlateB(Number2);
            LicensePlateB beforeC = new LicensePlateB(Number1);

            Console.WriteLine("a = b : " + (beforeA == beforeB));
            Console.WriteLine("a = c : " + (beforeA == beforeC));
            Console.WriteLine("b = c : " + (beforeB == beforeC));

            Console.WriteLine();

            Console.WriteLine("===== After =====");
            LicensePlateA afterA = new LicensePlateA(Number1);
            LicensePlateA afterB = new LicensePlateA(Number2);
            LicensePlateA afterC = new LicensePlateA(Number1);

            Console.WriteLine("a = b : " + (afterA == afterB));
            Console.WriteLine("a = c : " + (afterA == afterC));
            Console.WriteLine("b = c : " + (afterB == afterC));

            Console.ReadLine();
        }
    }
}
