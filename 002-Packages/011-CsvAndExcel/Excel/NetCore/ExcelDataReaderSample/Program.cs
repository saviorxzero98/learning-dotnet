using ExcelDataReader;
using ExcelSampleData;
using Npgg;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace ExcelDataReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var contacts = ReadExcel<Contact>("Data/Sample.xlsx", "MyContact");

            ConsoleTable.Write(contacts);
        }


        #region Read Excel

        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static DataSet ReadExcel(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = GetReader(stream, filePath))
                {
                    if (reader == null)
                    {
                        return null;
                    }

                    DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    return result;
                }
            }
        }

        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        static DataTable ReadExcel(string filePath, string sheetName)
        {
            var dataset = ReadExcel(filePath);

            var datatable = dataset.Tables[sheetName];

            return datatable;
        }

        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static List<T> ReadExcel<T>(string filePath, string sheetName)
        {
            var dataset = ReadExcel(filePath);

            var datatable = dataset.Tables[sheetName];

            var results = DataTableHelper.ToObjectList<T>(datatable);
            return results;
        }

        /// <summary>
        /// 取得 Excel Reader
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static IExcelDataReader GetReader(FileStream stream, string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            IExcelDataReader reader = null;
            switch (extension)
            {
                case ".xls":
                    reader = ExcelReaderFactory.CreateBinaryReader(stream, new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = Encoding.GetEncoding("big5")
                    });
                    break;

                case ".xlsx":
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream, new ExcelReaderConfiguration());
                    break;

                case ".csv":
                    reader = ExcelReaderFactory.CreateCsvReader(stream, new ExcelReaderConfiguration()
                    {
                        FallbackEncoding = Encoding.GetEncoding("big5")
                    });
                    break;
            }

            return reader;
        }

        #endregion
    }
}
