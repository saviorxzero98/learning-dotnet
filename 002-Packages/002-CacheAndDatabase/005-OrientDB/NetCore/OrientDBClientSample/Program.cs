using System;
using Orient.Client;
using OrientDB.Net.ConnectionProtocols.Binary;
using OrientDB.Net.Core;
using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Core.Models;
using OrientDB.Net.Serializers.RecordCSVSerializer;

namespace OrientDBClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Demo2();
            //Demo();
        }

        static void Demo2()
        {
            IOrientServerConnection server = new OrientDBConfiguration()
                                                .ConnectWith<byte[]>()
                                                .Connect(new BinaryProtocol("localhost", "root", "25867890"))
                                                .SerializeWith.Serializer(new OrientDBRecordCSVSerializer())
                                                .CreateFactory()
                                                .CreateConnection();

            IOrientDatabaseConnection database = server.DatabaseConnect("graphdb", DatabaseType.Graph);
        }


        static void Demo()
        {
            using (var server = new OServer("localhost", 2424, "gss", "25867890"))
            {
                using (var dbContext = new ODatabase("localhost", 2424, "graphdb", ODatabaseType.Graph, "gss", "25867890"))
                {
                    //var result = dbContext.GetClusterIdFor("V");
                    var result = dbContext.Gremlin("g.V().hasLabel('部門').count()");
                }
            }
        }
    }
}
