using CsvHelper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvHelperSample.Readers
{
    public class CsvFileReader
    {
        /// <summary>
        /// 讀取 CSV 檔案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<T> Read<T>(string fileName) where T : class
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<T>()
                                 .ToList();

                return records;
            }
        }

        /// <summary>
        /// 讀取 CSV 檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable Read(string fileName)
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
        /// 讀取 CSV 檔案
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public JToken ReadCsvToJToken(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                return JToken.FromObject(records);
            }
        }
    }
}
