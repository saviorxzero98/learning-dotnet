using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Data;

namespace NpoiSample.Extensions
{
    public static class NpoiExtensions
    {
        /// <summary>
        /// Workbook to DataSet
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataSet AsDataSet(this IWorkbook workbook, ExcelDataTableOptions options = null)
        {
            DataSet dataSet = new DataSet();

            int sheetCount = workbook.NumberOfSheets;
            if (sheetCount == 0)
            {
                return dataSet;
            }

            for (int i = 0; i < sheetCount; i++)
            {
                var sheet = workbook.GetSheetAt(i);

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
        public static DataTable AsDataTable(this ISheet sheet, ExcelDataTableOptions options = null)
        {
            options = options ?? new ExcelDataTableOptions();

            int rowCount = sheet.LastRowNum;
            if (rowCount == 0)
            {
                return null;
            }

            string sheetName = sheet.SheetName;
            var dataTable = new DataTable(sheetName);

            int rowDataIndex = 0;
            if (options.UseHeaderRow)
            {
                var row = sheet.GetRow(rowDataIndex++);
                var dataColumns = row.AsDataColumns();
                dataTable.Columns.AddRange(dataColumns.ToArray());
            }

            for (int i = rowDataIndex; i <= rowCount; i++)
            {
                var dataRow = dataTable.NewRow();
                var row = sheet.GetRow(i);
                
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
        public static List<DataColumn> AsDataColumns(this IRow row)
        {
            var columns = new List<DataColumn>();

            var columnCount = row.LastCellNum;

            for (int i = 0; i < columnCount; i++)
            {
                ICell cell = row.GetCell(i);
                columns.Add(new DataColumn(cell.StringCellValue));
            }
            return columns;
        }

        /// <summary>
        /// Fill Data Row
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="row"></param>
        public static void FillDataRow(DataRow dataRow, IRow row, ExcelDataTableOptions options)
        {
            if (row != null)
            {
                var columnCount = row.LastCellNum;

                for (int i = 0; i < columnCount; i++)
                {
                    ICell cell = row.GetCell(i);
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
        public static string GetCellValue(this ICell cell, ExcelDataTableOptions options)
        {
            var cellType = cell.CellType;

            switch (cellType)
            {
                case CellType.Numeric:
                    
                    if (DateUtil.IsCellDateFormatted(cell))
                    {
                        return cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        return cell.NumericCellValue.ToString();
                    }
                    
                case CellType.String:
                    return cell.StringCellValue;

                case CellType.Formula:
                    if (options != null && options.Evaluator != null)
                    {
                        var formulaValue = options.Evaluator.Evaluate(cell);
                        return formulaValue.ToString();
                    }
                    break;
            }
            return cell.ToString();
        }
    }

    public class ExcelDataTableOptions
    {
        public bool UseHeaderRow { get; set; } = false;

        public BaseFormulaEvaluator Evaluator;
    }
}
