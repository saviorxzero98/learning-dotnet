using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace DapperSample
{
    static class Program
    {
        static void Main()
        {
            
            using (var cn = new SQLiteConnection("Data source=SQL/Sample.db"))
            {
                cn.Open();

                string sql = "SELECT * FROM Customer";

                List<Customer> result = new List<Customer>(cn.Query<Customer>(sql));

                foreach(var customer in result)
                {
                    Console.WriteLine($"ID：{customer.Id}；Name：{customer.Name}");
                }

                cn.Close();
            }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreatDate { get; set; }
        }
    }
}
