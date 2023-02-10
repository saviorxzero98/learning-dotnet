using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer
{
    class Program
    {
        const string QueueName = "Demo_Queue";

        static void Main(string[] args)
        {
            RunStart();
        }

        static void RunStart()
        {
            var factory = CreateConnectionFactory();

            while (true)
            {
                using (var connection = factory.CreateConnection())
                {
                    var channel = connection.CreateModel();

                    channel.QueueDeclare(QueueName, true, false, false);

                    Console.WriteLine("輸入生產內容：");
                    string userInput = Console.ReadLine();

                    channel.BasicPublish(string.Empty, QueueName, null, Encoding.UTF8.GetBytes(userInput));
                }
            }
        }

        static ConnectionFactory CreateConnectionFactory()
        {
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                Port = 5672,  //RabbitMQ預設的埠
            };
            return factory;
        }
    }
}
