using ConsoleTables;
using CsvHelper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvHelperSample
{
    class Program
    {
        static void Main(string[] args)
        {
            const string csvFile = "Data.csv";

            Console.WriteLine("===== CSV to Model =====");
            var result01 = ReadCsvToModel(csvFile);
            PrintTable(result01);

            Console.WriteLine("\n===== CSV to Model (指定 Header) =====");
            var result02 = ReadCsvToModelWithHeader(csvFile);
            PrintTable(result02);

            Console.WriteLine("\n===== CSV to DataTable =====");
            var result03 = ReadCsvToDataTable(csvFile);
            PrintTable(result03);

            Console.WriteLine("\n===== CSV to JSON =====");
            var result04 = ReadCsvToJson(csvFile);
            Console.WriteLine(result04.ToString());


            Console.WriteLine("\n===== CSV to JSON =====");
            var result05 = ReadCsvToJson2(csvFile);
        }


        /// <summary>
        /// CSV to Model
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static List<WebSiteInfo> ReadCsvToModel(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<WebSiteInfo>()
                                 .ToList();

                return records;
            }
        }

        /// <summary>
        /// CSV to Model (指定 Header)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static List<WebSiteInfo> ReadCsvToModelWithHeader(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<WebSiteInfo>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new WebSiteInfo
                    {
                        Id = csv.GetField<int>(nameof(WebSiteInfo.Id)),
                        Name = csv.GetField(nameof(WebSiteInfo.Name)),
                        Url = csv.GetField(nameof(WebSiteInfo.Url))
                    };
                    records.Add(record);
                }

                return records;
            }
        }

        /// <summary>
        /// CSV to DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static DataTable ReadCsvToDataTable(string fileName)
        {
            var data = new DataTable();

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            using (var dataReader = new CsvDataReader(csv))
            {
                data.Load(dataReader);
            }
            return data;
        }

        /// <summary>
        /// CSV to Json Object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static JToken ReadCsvToJson(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                return JToken.FromObject(records);
            }
        }

        /// <summary>
        /// CSV to Json Object
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static List<JObject> ReadCsvToJson2(string fileName)
        {
            var results = ReadCsvToJson(fileName);
            return results.ToObject<List<JObject>>();
        }

        static void PrintTable<T>(IEnumerable<T> data)
        {
            var table = ConsoleTable.From(data);
            table.Configure(o => o.EnableCount = false)
                 .Configure(o => o.NumberAlignment = Alignment.Right)
                 .Write();
            Console.WriteLine();
        }

        static void PrintTable(DataTable dataTable)
        {
            List<string> headers = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                headers.Add(column.ColumnName);
            }

            var table = new ConsoleTable(headers.ToArray());

            for (int r = 0; r < dataTable.Rows.Count; r++)
            {
                List<object> rowValues = new List<object>();

                for (int c = 0; c < dataTable.Columns.Count; c++)
                {
                    rowValues.Add(dataTable.Rows[r][c]);
                }

                table.AddRow(rowValues.ToArray());
            }

            table.Configure(o => o.EnableCount = false)
                 .Configure(o => o.NumberAlignment = Alignment.Right)
                 .Write();
            Console.WriteLine();
        }
    }
}
