using BenchmarkDotNet.Running;
using DataTableSample.DataLoaders;
using DataTableUtils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<DataDistinctSample>();


            //DemoAddColumn();

            //var data = ReadData();
            DemoWithoutDistinctData(data.Copy());
            DemoDistinctData2(data.Copy());
            DemoDistinctData3(data.Copy());
            DemoDistinctData4(data.Copy());
            //DemoDistinctData(data.Copy());
        }


        static void DemoAddColumn()
        {
            var table = new DataTable();

            table = table.AddColumn<string>("Name");
            table = table.AddColumn<int>("Id", 1);
            table = table.AddColumnWithExpression<long>("Exp", "Id * Id");


            table.Rows.Add("ace");
            table.Rows.Add("jack", 11);
            table.Rows.Add("queen", 12);
            table.Rows.Add("king", 13);
        }

        static void DemoWithoutDistinctData(DataTable data)
        {
            DateTime start = DateTime.Now;

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var outData = view.ToTable(false, columns.ToArray());

            DateTime end = DateTime.Now;

            Console.WriteLine($"Column[{data.Columns.Count} --> {outData.Columns.Count}], Row[{data.Rows.Count} --> {outData.Rows.Count}], Total: {(end - start).TotalMilliseconds} ms <Without Distinct>");
        }

        static void DemoDistinctData(DataTable data)
        {
            DateTime start = DateTime.Now;

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var outData = view.ToTable(true, columns.ToArray());

            DateTime end = DateTime.Now;

            Console.WriteLine($"Column[{data.Columns.Count} --> {outData.Columns.Count}], Row[{data.Rows.Count} --> {outData.Rows.Count}], Total: {(end - start).TotalMilliseconds} ms <Distinct By DataView>");
        }

        static void DemoDistinctData2(DataTable data)
        {
            DateTime start = DateTime.Now;

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var tempData = view.ToTable(false, columns.ToArray());

            List<DataRow> outRows = tempData.AsEnumerable()
                                            .GroupBy(d => string.Join(",", d.ItemArray))
                                            .Select(d => d.First())
                                            .Where(d => d != null)
                                            .ToList();

            DataTable outData = tempData.Clone();
            foreach (var row in outRows)
            {
                outData.ImportRow(row);
            }

            DateTime end = DateTime.Now;

            Console.WriteLine($"Column[{data.Columns.Count} --> {outData.Columns.Count}], Row[{data.Rows.Count} --> {outData.Rows.Count}], Total: {(end - start).TotalMilliseconds} ms <Group By CSV String>");
        }

        static void DemoDistinctData3(DataTable data)
        {
            DateTime start = DateTime.Now;

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var tempData = view.ToTable(false, columns.ToArray());

            List<DataRow> outRows = tempData.AsEnumerable()
                                            .Distinct(DataRowComparer.Default)
                                            .ToList();

            DataTable outData = tempData.Clone();
            foreach (var row in outRows)
            {
                outData.ImportRow(row);
            }

            DateTime end = DateTime.Now;

            Console.WriteLine($"Column[{data.Columns.Count} --> {outData.Columns.Count}], Row[{data.Rows.Count} --> {outData.Rows.Count}], Total: {(end - start).TotalMilliseconds} ms <Distinct By DataRow>");
        }

        static void DemoDistinctData4(DataTable data)
        {
            DateTime start = DateTime.Now;

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var tempData = view.ToTable(false, columns.ToArray());


            List<string> outRecords = tempData.ToStringList()
                                              .Select(r => r)
                                              .Distinct()
                                              .ToList();

            string dataJson = JsonConvert.SerializeObject(outRecords.Select(s => JObject.Parse(s)).ToList());
            DataTable outData = (DataTable)JsonConvert.DeserializeObject(dataJson, (typeof(DataTable)));

            DateTime end = DateTime.Now;

            Console.WriteLine($"Column[{data.Columns.Count} --> {outData.Columns.Count}], Row[{data.Rows.Count} --> {outData.Rows.Count}], Total: {(end - start).TotalMilliseconds} ms <Distinct By JSON String>");
        }

        static DataTable ReadData()
        {
            string fileName = "TestData/Data.csv";

            var loader = new CsvDataLoader();
            DataTable data = loader.Read(fileName);

            return data;
        }
    }
}
