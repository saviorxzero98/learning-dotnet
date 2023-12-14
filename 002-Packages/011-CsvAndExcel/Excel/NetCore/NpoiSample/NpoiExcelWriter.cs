using ExcelLibSampleCommon;
using Newtonsoft.Json.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace NpoiSample
{
    public class NpoiExcelWriter : IExcelWriter
    {
        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        public void Write(Stream stream, DataSet data, ExcelFormat format = ExcelFormat.Xlsx)
        {
            IWorkbook workbook = GetWorkBook(format);
            for (int t = 0; t < data.Tables.Count; t++)
            {
                DataTable table = data.Tables[t];
                Write(workbook, table);
            }
            workbook.Write(stream);
        }

        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        public void Write(Stream stream, DataTable data, ExcelFormat format = ExcelFormat.Xlsx)
        {
            IWorkbook workbook = GetWorkBook(format);
            Write(workbook, data);
            workbook.Write(stream);
        }

        /// <summary>
        /// 寫入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="sheetName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public void Write<T>(Stream stream, List<T> data, string sheetName, ExcelFormat format = ExcelFormat.Xlsx)
        {
            var dataTable = DataTableHelper.ToDataTable(data, sheetName);
            Write(stream, dataTable, format);
        }

        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        protected void Write(IWorkbook workbook, DataTable table)
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
                    var cellValue = JToken.FromObject(dataRow[c]);

                    switch (cellValue.Type)
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



        #region Private

        /// <summary>
        /// 取得 Workbook
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected IWorkbook GetWorkBook(ExcelFormat format)
        {
            IWorkbook workbook;
            switch (format)
            {
                case ExcelFormat.Xls:
                    workbook = new HSSFWorkbook();
                    break;

                case ExcelFormat.Xlsx:
                default:
                    workbook = new XSSFWorkbook();
                    break;
            }

            return workbook;
        }

        #endregion
    }
}
