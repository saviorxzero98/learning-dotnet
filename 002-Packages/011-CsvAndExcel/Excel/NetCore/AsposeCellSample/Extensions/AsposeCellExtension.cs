using Aspose.Cells;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

namespace AsposeCellSample.Extensions
{
    public static class AsposeCellExtension
    {
        #region Workbook to DataSet / DataTable

        /// <summary>
        /// Workbook to DataSet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataSet AsDataSet(this Workbook workbook, ExcelDataTableOptions options = null)
        {
            DataSet dataSet = new DataSet();

            int sheetCount = workbook.Worksheets.Count;
            if (sheetCount == 0)
            {
                return dataSet;
            }

            for (int i = 0; i < sheetCount; i++)
            {
                var sheet = workbook.Worksheets[i];

                if (sheet == null)
                {
                    continue;
                }

                var sheetData = sheet.AsDataTable(options);
                if (sheetData != null)
                {
                    dataSet.Tables.Add(sheetData);
                }
            }
            return dataSet;
        }

        /// <summary>
        /// Sheet to DataTable
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataTable AsDataTable(this Worksheet sheet, ExcelDataTableOptions options = null)
        {
            options = options ?? new ExcelDataTableOptions();

            var cells = sheet.Cells;
            var dataTable = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, options.UseHeaderRow);
            dataTable.TableName = sheet.Name;
            return dataTable;
        }

        #endregion


        #region DataTable to Workbook

        /// <summary>
        /// Add Sheet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="table"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static Worksheet AddSheet(this Workbook workbook, DataTable table, WorksheetBuildSetting setting = null)
        {
            var sheet = workbook.Worksheets.Add(table.TableName);
            var sheetSetting = setting ?? new WorksheetBuildSetting();

            // 建立 Header Row
            for (int col = 0; col < table.Columns.Count; col++)
            {
                var rowIndex = 0;
                var cell = sheet.Cells[rowIndex, col];
                var columnName = table.Columns[col].ToString();
                cell.PutValue(columnName);

                // 將欄寬變更為最合適的大小
                sheet.AutoFitColumn(col);
            }

            // 建立 Rows
            for (int row = 0; row < table.Rows.Count; row++)
            {
                var rowIndex = row + 1;
                DataRow dataRow = table.Rows[row];

                for (int col = 0; col < table.Columns.Count; col++)
                {
                    var cell = sheet.Cells[rowIndex, col];
                    var cellValue = JToken.FromObject(dataRow[col]);

                    // 檢查是否為空
                    if (cellValue == null)
                    {
                        cell.PutValue(string.Empty);
                        continue;
                    }

                    if (cellValue is JValue)
                    {
                        // 值為一個結構
                        switch (cellValue.Type)
                        {
                            case JTokenType.Integer:
                                cell.PutValue(cellValue.ToObject<long>());
                                break;

                            case JTokenType.Float:
                                cell.PutValue(cellValue.ToObject<double>());
                                break;

                            case JTokenType.Boolean:
                                cell.PutValue(cellValue.ToObject<bool>() ? sheetSetting.CellTrueValue : sheetSetting.CellFalseValue);
                                break;

                            case JTokenType.Date:
                                cell.PutValue(cellValue.ToObject<DateTime>().ToString(sheetSetting.DateTimeFormat));
                                break;

                            case JTokenType.Guid:
                            case JTokenType.String:
                            default:
                                cell.PutValue(cellValue.ToString());
                                break;
                        }
                    }
                    else if (cellValue is JArray)
                    {
                        // 值為一個陣列
                        var arrayValue = string.Join(sheetSetting.ArraySeparatedChar,
                                                     ((JArray)cellValue).ToList()
                                                                        .Where(v => v != null)
                                                                        .Select(v => v.ToString()));
                            
                        cell.PutValue(arrayValue);
                    }
                    else
                    {
                        // 其他
                        cell.PutValue(cellValue.ToString());
                    }
                }
            }
            return sheet;
        }

        /// <summary>
        /// 新增一個 Sheet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workbook"></param>
        /// <param name="sheetName"></param>
        /// <param name="rows"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static Worksheet AddSheet<T>(this Workbook workbook, string sheetName, List<T> rows,
                                            WorksheetBuildSetting setting = null) where T : class
        {
            var datatable = ToDataTable(rows, sheetName);
            return workbook.AddSheet(datatable, setting);
        }

        /// <summary>
        /// List 轉換成 DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(List<T> items, string tableName) where T : class
        {
            DataTable dataTable = new DataTable(tableName);

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 定義 Data Columns
            foreach (PropertyInfo property in properties)
            {
                var type = property.PropertyType;

                // 檢查是否為 Null
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = Nullable.GetUnderlyingType(property.PropertyType);
                }

                dataTable.Columns.Add(property.Name, type);
            }

            // 設定 Data Rows
            foreach (T item in items)
            {
                var values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            // Columns 排序
            foreach (PropertyInfo property in properties)
            {
                var propertyName = property.Name;
            }
            return dataTable;
        }

        #endregion
    }

    public class ExcelDataTableOptions
    {
        public bool UseHeaderRow { get; set; } = false;
    }


    public class WorksheetBuildSetting
    {

        public const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public const string DefaultCellTrueValue = "true";

        public const string DefaultCellFalseValue = "false";

        public const string DefaultArraySeparatedChar = ",";


        /// <summary>
        /// 日期時間的格式
        /// </summary>
        public string DateTimeFormat { get; set; } = DefaultDateTimeFormat;

        /// <summary>
        /// 儲存格為 true 的格式
        /// </summary>
        public string CellTrueValue { get; set; } = DefaultCellTrueValue;

        /// <summary>
        /// 儲存格為 false 的格式
        /// </summary>
        public string CellFalseValue { get; set; } = DefaultCellFalseValue;

        /// <summary>
        /// 陣列值分隔字元
        /// </summary>
        public string ArraySeparatedChar { get; set; } = DefaultArraySeparatedChar;
    }
}
