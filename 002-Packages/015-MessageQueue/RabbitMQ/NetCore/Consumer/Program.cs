using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
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

            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();

                channel.QueueDeclare(QueueName, true, false, false);

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                consumer.Received += (a, e) =>
                {
                    Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] " + Encoding.UTF8.GetString(e.Body.ToArray()));
                    channel.BasicAck(e.DeliveryTag, true); //收到回覆後，RabbitMQ會直接在佇列中刪除這條訊息
                };
                channel.BasicConsume(QueueName, false, consumer);

                Console.WriteLine("啟動成功");
                Console.ReadLine();
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
