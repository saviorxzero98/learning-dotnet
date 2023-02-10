using Newtonsoft.Json.Linq;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqlKataSample
{
    public class SqlExecutionSample
    {
        public void DemoSqlExecutionBySqlServer()
        {
            string connectionString = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=ChatBot;Application Name=ChatBot;";
            using (var cn = new SqlConnection(connectionString))
            {
                //var results = SelectFirst(cn, new SqlServerCompiler());
                var botId = Insert(cn, new SqlServerCompiler());
                var result = Update(cn, new SqlServerCompiler(), botId);
            }
        }


        public BotStateStoreDao SelectFirst(IDbConnection connection, Compiler compiler)
        {
            var db = new QueryFactory(connection, compiler);
            var result = db.Query("BotStateData").Select("*")
                                                 .Where(nameof(BotStateStoreDao.BotId), "655")
                                                 .OrderByDesc("Timestamp")
                                                 .FirstOrDefault<BotStateStoreDao>();
            return result;
        }

        public List<BotStateStoreDao> SelectAll(IDbConnection connection, Compiler compiler)
        {
            var query = new Query("BotStateData").Select("*")
                                                 .OrderByDesc("Timestamp");


            var db = new QueryFactory(connection, compiler);
            var results = db.FromQuery(query)
                            .Get<BotStateStoreDao>()
                            .ToList();
            return results;
        }

        public string Insert(IDbConnection connection, Compiler compiler)
        {
            var botId = Guid.NewGuid().ToString();
            var data = new BotStateStoreDao()
            {
                BotId = botId,
                StateKey = Guid.NewGuid().ToString(),
                BotData = JObject.Parse("{a: \"abc\", b: 56}"),
                UpdateDate = DateTime.Now
            };

            var query = new Query("BotStateData");

            var db = new QueryFactory(connection, compiler);
            var result = db.FromQuery(query).Insert(new
            {
                BotId = data.BotId,
                StateKey = data.StateKey,
                Data = data.Data,
                Timestamp = data.TimeStamp
            });
            return botId;
        }
        
        public int Update(IDbConnection connection, Compiler compiler, string botId)
        {
            var data = new
            {
                StateKey = $"Update-{DateTime.Now}",
                Timestamp = DateTime.Now
            };
            var dict = new Dictionary<string, object>()
            {
                { "StateKey", $"Update-{DateTime.Now}" },
                { "Timestamp", DateTime.Now }
            };

            var query = new Query("BotStateData").Where("BotId", botId);

            var db = new QueryFactory(connection, compiler);
            var result = db.FromQuery(query).Update(data);
            return result;
        }
    }
}
