using Autofac;
using System;
using System.Threading;

namespace CS_Autofac
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();

            //註冊時加註SingleInstance()，Autofac便會以Singleton方式提供物件
            builder.RegisterType<UuidCreater>().As<IIdCreater>().SingleInstance();

            IContainer container = builder.Build();

            for (int i = 0; i < 3; i++)
            {
                ThreadPool.QueueUserWorkItem((o) =>
                {
                    var uuid = container.Resolve<IIdCreater>();

                    uuid.Print();
                });
            }

            Console.ReadLine();
        }
    }
}
