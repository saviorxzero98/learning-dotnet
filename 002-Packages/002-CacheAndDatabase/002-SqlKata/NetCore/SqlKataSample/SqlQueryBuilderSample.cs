using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SqlKata;
using SqlKata.Compilers;
using System;

namespace SqlKataSample
{
    public class SqlQueryBuilderSample
    {
        public void RunDemo()
        {
            Console.WriteLine("====================\nSelect\n");

            DemoSelectSQL(new SqlServerCompiler(), "SQL Server");
            DemoSelectSQL(new SqliteCompiler(), "Sqlite");
            DemoSelectSQL(new PostgresCompiler(), "Postgres");
            DemoSelectSQL(new MySqlCompiler(), "MySql");

            Console.WriteLine("\n====================\nInsert\n");

            DemoInsertSQL(new SqlServerCompiler(), "SQL Server");
            DemoInsertSQL(new SqliteCompiler(), "Sqlite");
            DemoInsertSQL(new PostgresCompiler(), "Postgres");
            DemoInsertSQL(new MySqlCompiler(), "MySql");

            Console.WriteLine("\n====================\nUpdate\n");

            DemoUpdateSQL(new SqlServerCompiler(), "SQL Server");
            DemoUpdateSQL(new SqliteCompiler(), "Sqlite");
            DemoUpdateSQL(new PostgresCompiler(), "Postgres");
            DemoUpdateSQL(new MySqlCompiler(), "MySql");

            Console.WriteLine("\n====================\nDelete\n");

            DemoDeleteSQL(new SqlServerCompiler(), "SQL Server");
            DemoDeleteSQL(new SqliteCompiler(), "Sqlite");
            DemoDeleteSQL(new PostgresCompiler(), "Postgres");
            DemoDeleteSQL(new MySqlCompiler(), "MySql");
        }



        public void DemoSelectSQL(Compiler compiler, string title)
        {
            var query = new Query("BotStateData").Select("*")
                                                 .Where("StateKey", "abc")
                                                 .Where("BotId", "myBot")
                                                 .OrderByDesc("Timestamp")
                                                 .Limit(1);

            var sql = compiler.Compile(query).Sql;
            var sqlParam = compiler.Compile(query).NamedBindings;


            Console.WriteLine($"{title}\n {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }

        public void DemoInsertSQL(Compiler compiler, string title)
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


            Console.WriteLine($"{title}\n {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }

        public void DemoUpdateSQL(Compiler compiler, string title)
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


            Console.WriteLine($"{title}\n {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }

        public void DemoDeleteSQL(Compiler compiler, string title)
        {
            var query = new Query("BotStateData").Where("StateKey", "=", "abc")
                                                 .Where("BotId", "myBot")
                                                 .AsDelete();

            var sql = compiler.Compile(query).Sql;
            var sqlParam = compiler.Compile(query).NamedBindings;


            Console.WriteLine($"{title}\n {sql}");
            Console.WriteLine($"{JsonConvert.SerializeObject(sqlParam)}\n");
        }
    }
}
