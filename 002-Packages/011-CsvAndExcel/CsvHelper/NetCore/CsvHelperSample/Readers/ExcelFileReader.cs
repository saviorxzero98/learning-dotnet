using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Excel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace CsvHelperSample.Readers
{
    public class ExcelFileReader
    {
        /// <summary>
        /// 讀取 CSV 檔案
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public List<T> Read<T>(string fileName, string sheetName) where T : class
        {
            using (var parser = new ExcelParser(fileName, sheetName))
            using (var csv = new CsvReader(parser))
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
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public DataTable Read(string fileName, string sheetName)
        {
            var data = new DataTable();

            using (var parser = new ExcelParser(fileName, sheetName))
            using (var csv = new CsvReader(parser))
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
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public JToken ReadExcelToJToken(string fileName, string sheetName)
        {
            using (var parser = new ExcelParser(fileName, sheetName))
            using (var csv = new CsvReader(parser))
            {
                var records = csv.GetRecords<dynamic>();
                return JToken.FromObject(records);
            }
        }
    }
}
