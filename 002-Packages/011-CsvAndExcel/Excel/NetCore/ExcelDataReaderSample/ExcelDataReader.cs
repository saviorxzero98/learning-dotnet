using ExcelDataReader;
using ExcelLibSampleCommon;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace ExcelDataReaderSample
{
    public class ExcelDataReader : IExcelReader
    {
        public ExcelReaderConfiguration Configuration { get; set; }

        public ExcelDataReader()
        {

        }
        public ExcelDataReader(ExcelReaderConfiguration config)
        {
            Configuration = config;
        }


        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet Read(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = GetReader(stream, filePath))
            {
                if (reader == null)
                {
                    return null;
                }

                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true,
                    }
                });

                return result;
            }
        }

        /// <summary>
        /// 讀取 Excel (指定 Sheet)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public DataTable Read(string filePath, string sheetName)
        {
            var dataset = Read(filePath);
            var datatable = dataset.Tables[sheetName];
            return datatable;
        }

        /// <summary>
        /// 讀取 Excel (指定 Sheet)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public List<T> Read<T>(string filePath, string sheetName) where T : class
        {
            var dataset = Read(filePath);
            var datatable = dataset.Tables[sheetName];
            var results = DataTableHelper.ToObjectList<T>(datatable);
            return results;
        }


        /// <summary>
        /// 依據副檔名取得對應的 Reader
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected IExcelDataReader GetReader(FileStream stream, string filePath)
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
    }
}
