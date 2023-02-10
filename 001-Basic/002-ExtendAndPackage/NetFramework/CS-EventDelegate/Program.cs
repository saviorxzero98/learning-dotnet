using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Before.Sample before = new Before.Sample();
            before.RunSample();

            Sample01.Sample sample01 = new Sample01.Sample();
            sample01.RunSample();

            Sample02.Sample sample02 = new Sample02.Sample();
            sample02.RunSample();

            Sample03.Sample sample03 = new Sample03.Sample();
            sample03.RunSample();

            Console.ReadLine();
        }
    }
}
