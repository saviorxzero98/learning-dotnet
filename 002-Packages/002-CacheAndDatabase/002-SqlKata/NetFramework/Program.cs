using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlKata;
using SqlKata.Compilers;
using System;

namespace CS_SqlKata
{
    class Program
    {
        public static string ConnectionString { get; private set; }

        static void Main(string[] args)
        {
            Console.WriteLine("====================\nSelect\n");

            DemoSelectSQL(new SqlServerCompiler(), "MsSql");
            DemoSelectSQL(new SqliteCompiler(), "Sqlite");
            DemoSelectSQL(new PostgresCompiler(), "Postgres");
            DemoSelectSQL(new MySqlCompiler(), "MySql");

            Console.WriteLine("\n====================\nInsert\n");

            DemoInsertSQL(new SqlServerCompiler(), "MsSql");
            DemoInsertSQL(new SqliteCompiler(), "Sqlite");
            DemoInsertSQL(new PostgresCompiler(), "Postgres");
            DemoInsertSQL(new MySqlCompiler(), "MySql");

            Console.WriteLine("\n====================\nUpdate\n");

            DemoUpdateSQL(new SqlServerCompiler(), "MsSql");
            DemoUpdateSQL(new SqliteCompiler(), "Sqlite");
            DemoUpdateSQL(new PostgresCompiler(), "Postgres");
            DemoUpdateSQL(new MySqlCompiler(), "MySql");

            Console.WriteLine("\n====================\nDelete\n");

            DemoDeleteSQL(new SqlServerCompiler(), "MsSql");
            DemoDeleteSQL(new SqliteCompiler(), "Sqlite");
            DemoDeleteSQL(new PostgresCompiler(), "Postgres");
            DemoDeleteSQL(new MySqlCompiler(), "MySql");
        }


        static void DemoSelectSQL(Compiler compiler, string title)
        {
            var query = new Query("BotStateData").Select("*")
                                                 .Where("StateKey", "abc")
                                                 .Where("BotId", "myBot")
                                                 .OrderByDesc("Timestamp")
                                                 .Limit(1);

            var sql = compiler.Compile(query).Sql;
            var sqlParam = compiler.Compile(query).NamedBindings;


            Console.WriteLine($"{title}: {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }

        static void DemoInsertSQL(Compiler compiler, string title)
        {
            var data = new BotStateStoreDao()
            {
                BotId = "MyBot",
                StateKey = "abc",
                BotData = JObject.Parse("{a: \"abc\", b: 56}"),
                UpdateDate = DateTime.Now
            };

            var query = new Query("BotStateData").AsInsert(new
            {
                BotId = data.BotId,
                StateKey = data.StateKey,
                Data = data.Data,
                Timestamp = data.TimeStamp
            });

            var sql = compiler.Compile(query).Sql;
            var sqlParam = compiler.Compile(query).NamedBindings;


            Console.WriteLine($"{title}: {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }

        static void DemoUpdateSQL(Compiler compiler, string title)
        {
            var query = new Query("BotStateData").Where("StateKey", "=", "abc")
                                                 .Where("BotId", "myBot")
                                                 .AsUpdate(new
                                                 {
                                                    Data = "",
                                                    Timestamp = DateTime.Now
                                                 });

            var sql = compiler.Compile(query).Sql;
            var sqlParam = compiler.Compile(query).NamedBindings;


            Console.WriteLine($"{title}: {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }

        static void DemoDeleteSQL(Compiler compiler, string title)
        {
            var query = new Query("BotStateData").Where("StateKey", "=", "abc")
                                                 .Where("BotId", "myBot")
                                                 .AsDelete();

            var sql = compiler.Compile(query).Sql;
            var sqlParam = compiler.Compile(query).NamedBindings;


            Console.WriteLine($"{title}: {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }
    }
}
