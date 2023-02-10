using System;
using System.Threading;

namespace GenericMethod.Item
{
    public class Note7 : Computer
    {
        public Note7(int price)
        {

        }

        public void Boot()
        {
            Console.WriteLine("Boot Note 7 - Start Count Down");

            for(int i=10; i>0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Explode Explode Explode Explode");
            throw new Exception();
        }
    }
}
