using Aspose.Cells;
using AsposeCellSample.Extensions;
using ExcelLibSampleCommon;
using System.Data;

namespace AsposeCellSample
{
    public class AsposeCellExcelReader : IExcelReader
    {
        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet Read(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                Workbook workbook = GetWorkBook(stream, filePath);

                DataSet dataSet = workbook.AsDataSet(new ExcelDataTableOptions()
                {
                    UseHeaderRow = true
                });

                return dataSet;
            }
        }
        /// <summary>
        /// 
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


        #region Private

        /// <summary>
        /// 取得 Workbook
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected Workbook GetWorkBook(string filePath)
        {
            return new Workbook(filePath);
        }

        /// <summary>
        /// 取得 Workbook
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected Workbook GetWorkBook(Stream stream, string filePath)
        {
            return new Workbook(stream);
        }


        #endregion
    }
}
