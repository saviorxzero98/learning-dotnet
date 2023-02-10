using ExcelSampleData;
using Newtonsoft.Json.Linq;
using Npgg;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NpoiSample.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NpoiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var contacts = ReadExcel<Contact>("Data/Sample.xlsx", "MyContact");

            ConsoleTable.Write(contacts);

            WriteExcel("Data/NewSample.xlsx", contacts, "MyContact");
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

        #endregion


        #region Write Excel

        static void WriteExcel(string filePath, DataSet data)
        {
            IWorkbook workbook = GetWorkBook(filePath);
            for (int t = 0; t < data.Tables.Count; t++)
            {
                DataTable table = data.Tables[t];
                Write(workbook, table);
            }

            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }
        }

        static void WriteExcel(string filePath, DataTable data)
        {
            IWorkbook workbook = GetWorkBook(filePath);
            Write(workbook, data);

            using (var fileData = new FileStream(filePath, FileMode.Create))
            {
                workbook.Write(fileData);
            }
        }

        static void WriteExcel<T>(string filePath, List<T> data, string sheetName)
        {
            var datatable = DataTableHelper.ToDataTable(data, sheetName);
            
            WriteExcel(filePath, datatable);
        }

        static void Write(IWorkbook workbook, DataTable table)
        {
            ISheet sheet = workbook.CreateSheet(table.TableName);

            IRow headerRow = sheet.CreateRow(0);
            for (int c = 0; c < table.Columns.Count; c++)
            {
                ICell cell = headerRow.CreateCell(c);
                string columnName = table.Columns[c].ToString();
                cell.SetCellValue(columnName);
            }

            for (int r = 0; r < table.Rows.Count; r++)
            {
                IRow row = sheet.CreateRow(r + 1);
                DataRow dataRow = table.Rows[r];
                for (int c = 0; c < table.Columns.Count; c++)
                {
                    ICell cell = row.CreateCell(c);
                    var cellValue = JValue.FromObject(dataRow[c]);

                    switch(cellValue.Type)
                    {
                        case JTokenType.Integer:
                            cell.SetCellValue(cellValue.ToObject<long>());
                            break;

                        case JTokenType.Float:
                            cell.SetCellValue(cellValue.ToObject<double>());
                            break;

                        case JTokenType.Boolean:
                            cell.SetCellValue(cellValue.ToObject<bool>());
                            break;

                        case JTokenType.Date:
                            cell.SetCellValue(cellValue.ToObject<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"));
                            break;

                        case JTokenType.String:
                        default:
                            cell.SetCellValue(cellValue.ToString());
                               break;
                    }
                }
            }
        }

        #endregion

        static IWorkbook GetWorkBook(string filePath)
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


        static IWorkbook GetWorkBook(Stream stream, string filePath)
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

        static BaseFormulaEvaluator GetFormulaEvaluator(IWorkbook workbook, string filePath)
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
    }
}
