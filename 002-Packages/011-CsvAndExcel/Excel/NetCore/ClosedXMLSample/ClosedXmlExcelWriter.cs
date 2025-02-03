using ClosedXML.Excel;
using ExcelLibSampleCommon;
using Newtonsoft.Json.Linq;
using System.Data;

namespace ClosedXMLSample
{
    public class ClosedXmlExcelWriter : IExcelWriter
    {
        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        public void Write(Stream stream, DataSet data, ExcelFormat format = ExcelFormat.Xlsx)
        {
            IXLWorkbook workbook = new XLWorkbook();
            for (int t = 0; t < data.Tables.Count; t++)
            {
                DataTable table = data.Tables[t];
                Write(workbook, table);
            }
            workbook.SaveAs(stream);
        }

        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        public void Write(Stream stream, DataTable data, ExcelFormat format = ExcelFormat.Xlsx)
        {
            IXLWorkbook workbook = new XLWorkbook();
            Write(workbook, data);
            workbook.SaveAs(stream);
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
        protected void Write(IXLWorkbook workbook, DataTable table)
        {
            IXLWorksheet sheet = workbook.AddWorksheet(table.TableName);

            IXLRow headerRow = sheet.FirstRow();
            for (int c = 0; c < table.Columns.Count; c++)
            {
                IXLCell cell = headerRow.Cell(c + 1);
                string columnName = table.Columns[c].ToString();
                cell.Value = columnName;
            }

            for (int r = 0; r < table.Rows.Count; r++)
            {
                IXLRow row = sheet.Row(r + 1 + 1);
                DataRow dataRow = table.Rows[r];
                for (int c = 0; c < table.Columns.Count; c++)
                {
                    IXLCell cell = row.Cell(c + 1);
                    var cellValue = JToken.FromObject(dataRow[c]);

                    switch (cellValue.Type)
                    {
                        case JTokenType.Integer:
                            cell.Value = cellValue.ToObject<long>();
                            break;

                        case JTokenType.Float:
                            cell.Value = cellValue.ToObject<double>();
                            break;

                        case JTokenType.Boolean:
                            cell.Value = cellValue.ToObject<bool>();
                            break;

                        case JTokenType.Date:
                            cell.Value = cellValue.ToObject<DateTime>().ToString("yyyy-MM-dd HH:mm:ss");
                            break;

                        case JTokenType.String:
                        default:
                            cell.Value = cellValue.ToString();
                            break;
                    }
                }
            }
        }
    }
}
