using System;
using System.Threading;

namespace CS_Autofac
{
    public class TimestampCreater : IIdCreater
    {
        private long Ticks = DateTime.Now.Ticks;


        public TimestampCreater()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Create Timestamp");
        }

        public void Print()
        {
            Console.WriteLine($"Timestamp: {Ticks}");
        }
    }
}
