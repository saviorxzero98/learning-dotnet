using ClosedXML.Excel;
using System.Data;

namespace ClosedXMLSample.Extensions
{
    public static class ClosedXmlExtensions
    {
        /// <summary>
        /// Workbook to DataSet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataSet AsDataSet(this IXLWorkbook workbook, ExcelDataTableOptions options = null)
        {
            DataSet dataSet = new DataSet();

            int sheetCount = workbook.Worksheets.Count;

            foreach (var sheet in workbook.Worksheets)
            {
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
        public static DataTable AsDataTable(this IXLWorksheet sheet, ExcelDataTableOptions options = null)
        {
            options = options ?? new ExcelDataTableOptions();

            int rowCount = sheet.LastRowUsed()?.RowNumber() ?? 0;
            if (rowCount == 0)
            {
                return null;
            }

            string sheetName = sheet.Name;
            var dataTable = new DataTable(sheetName);


            int rowDataIndex = 1;
            if (options.UseHeaderRow)
            {
                var row = sheet.Row(rowDataIndex++);
                var dataColumns = row.AsDataColumns();
                dataTable.Columns.AddRange(dataColumns.ToArray());
            }

            for (int i = rowDataIndex; i < rowCount; i++)
            {
                var dataRow = dataTable.NewRow();
                var row = sheet.Row(i);

                FillDataRow(dataRow, row, options);

                dataTable.Rows.Add(dataRow);
            }
            return dataTable;
        }

        /// <summary>
        /// Row to DataColumns
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static List<DataColumn> AsDataColumns(this IXLRow row)
        {
            var columns = new List<DataColumn>();

            var columnCount = row.CellsUsed().Count();

            for (int i = 1; i <= columnCount; i++)
            {
                IXLCell cell = row.Cell(i);
                columns.Add(new DataColumn(cell.GetText()));
            }
            return columns;
        }

        /// <summary>
        /// Fill Data Row
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="row"></param>
        public static void FillDataRow(DataRow dataRow, IXLRow row, ExcelDataTableOptions options)
        {
            if (row != null)
            {
                var columnCount = row.CellsUsed().Count();

                for (int i = 0; i < columnCount; i++)
                {
                    IXLCell cell = row.Cell(i + 1);
                    if (cell == null)
                    {
                        continue;
                    }
                    dataRow[i] = cell.GetCellValue(options);
                }
            }
        }

        /// <summary>
        /// Get Cell Value
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string GetCellValue(this IXLCell cell, ExcelDataTableOptions options)
        {
            var cellType = cell.DataType;

            switch (cellType)
            {
                case XLDataType.Number:
                    return cell.GetDouble().ToString();

                case XLDataType.DateTime:
                    return cell.GetDateTime().ToString("yyyy-MM-dd HH:mm:ss");

                case XLDataType.TimeSpan:
                    return cell.GetTimeSpan().ToString();

                case XLDataType.Boolean:
                    return cell.GetBoolean().ToString();

                case XLDataType.Text:
                    return cell.GetText();

                case XLDataType.Blank:
                    return string.Empty;
            }
            return cell.GetText();
        }
    }

    public class ExcelDataTableOptions
    {
        public bool UseHeaderRow { get; set; } = false;
    }
}
