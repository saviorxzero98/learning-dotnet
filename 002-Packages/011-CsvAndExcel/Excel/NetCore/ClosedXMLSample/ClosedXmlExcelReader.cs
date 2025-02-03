using ClosedXML.Excel;
using ClosedXMLSample.Extensions;
using ExcelLibSampleCommon;
using System.Data;
using static ClosedXMLSample.Extensions.ClosedXmlExtensions;

namespace ClosedXMLSample
{
    public class ClosedXmlExcelReader : IExcelReader
    {
        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public DataSet Read(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                DataSet dataSet = workbook.AsDataSet(new ExcelDataTableOptions()
                {
                    UseHeaderRow = true
                });

                return dataSet;
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
    }
}
