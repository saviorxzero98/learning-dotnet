using System;

namespace ExplicitAndImplicitSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Date.NextWeek.ToString());

            Date date = Date.NextWeek;

            // Date -> DateTime (Implicit)
            DateTime datetime = date;

            // DateTime -> Date (Explicit)
            date = (Date) datetime;
        }
    }
}
