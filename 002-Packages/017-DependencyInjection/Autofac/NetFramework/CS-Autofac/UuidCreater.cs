using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS_Autofac
{
    public class UuidCreater : IIdCreater
    {
        private Guid Uuid = Guid.NewGuid();


        public UuidCreater()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Create GUID");
        }

        public void Print()
        {
            Console.WriteLine($"Uuid: {Uuid}");
        }
    }
}
