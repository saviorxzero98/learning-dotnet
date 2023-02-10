using BenchmarkDotNet.Attributes;
using DataTableSample.DataLoaders;
using DataTableUtils.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataTableSample
{
    public class DataDistinctSample
    {

        [Benchmark(Description = "Without Distinct")]
        public void DemoWithoutDistinctData()
        {
            DataTable data = ReadData();

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var outData = view.ToTable(false, columns.ToArray());
        }

        //[Benchmark(Description = "Distinct By DataView")]
        public void DemoDistinctData()
        {
            DataTable data = ReadData();

            DataView view = new DataView(data);
            //List<string> columns = new List<string>();
            List<string> columns = new List<string>() { "Id", "Name", "Url" };
            var outData = view.ToTable(true, columns.ToArray());
        }

        [Benchmark(Description = "Group By CSV String")]
        public void DemoDistinctData2()
        {
            DataTable data = ReadData();

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
        }

        [Benchmark(Description = "Distinct By DataRow")]
        public void DemoDistinctData3()
        {
            DataTable data = ReadData();

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
        }

        [Benchmark(Description = "Distinct By JSON String")]
        public void DemoDistinctData4()
        {
            DataTable data = ReadData();

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
        }



        private DataTable ReadData()
        {
            string fileName = "TestData/Data.csv";

            var loader = new CsvDataLoader();
            DataTable data = loader.Read(fileName);

            return data;
        }
    }
}
