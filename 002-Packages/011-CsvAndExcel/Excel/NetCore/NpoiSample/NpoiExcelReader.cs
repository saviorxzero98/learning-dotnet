using ExcelLibSampleCommon;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NpoiSample.Extensions;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NpoiSample
{
    public class NpoiExcelReader : IExcelReader
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
                IWorkbook workbook = GetWorkBook(stream, filePath);
                BaseFormulaEvaluator evaluator = GetFormulaEvaluator(workbook, filePath);

                DataSet dataSet = workbook.AsDataSet(new ExcelDataTableOptions()
                {
                    UseHeaderRow = true,
                    Evaluator = evaluator
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


        #region Private

        /// <summary>
        /// 取得 Workbook
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected IWorkbook GetWorkBook(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            IWorkbook workbook = null;
            switch (extension)
            {
                case ".xlsx":
                    workbook = new XSSFWorkbook();
                    break;

                case ".xls":
                    workbook = new HSSFWorkbook();
                    break;
            }

            return workbook;
        }

        /// <summary>
        /// 取得 Workbook
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected IWorkbook GetWorkBook(Stream stream, string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            IWorkbook workbook = null;
            switch (extension)
            {
                case ".xlsx":
                    workbook = new XSSFWorkbook(stream);
                    break;

                case ".xls":
                    workbook = new HSSFWorkbook(stream);
                    break;
            }

            return workbook;
        }

        /// <summary>
        /// 取得 Formula
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected BaseFormulaEvaluator GetFormulaEvaluator(IWorkbook workbook, string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            BaseFormulaEvaluator evaluator = null;
            switch (extension)
            {
                case ".xlsx":
                    evaluator = new XSSFFormulaEvaluator(workbook);
                    break;

                case ".xls":
                    evaluator = new HSSFFormulaEvaluator(workbook);
                    break;
            }

            return evaluator;
        }

        #endregion
    }
}
