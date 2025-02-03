using Aspose.Cells;
using AsposeCellSample.Extensions;
using ExcelLibSampleCommon;
using System.Data;

namespace AsposeCellSample
{
    public class AsposeCellExcelWriter : IExcelWriter
    {
        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        public void Write(Stream stream, DataSet data, ExcelFormat format = ExcelFormat.Xlsx)
        {
            Workbook workbook = new Workbook(GetFileFormatType(format));

            if (data.Tables.Count != 0)
            {
                workbook.Worksheets.Clear();
            }
            
            for (int t = 0; t < data.Tables.Count; t++)
            {
                DataTable table = data.Tables[t];
                Write(workbook, table);
            }
            workbook.Save(stream, GetSaveFormat(format));
        }

        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        public void Write(Stream stream, DataTable data, ExcelFormat format = ExcelFormat.Xlsx)
        {
            var formatType = GetFileFormatType(format);
            Workbook workbook = new Workbook(formatType);
            workbook.Worksheets.Clear();
            Write(workbook, data);
            workbook.Save(stream, GetSaveFormat(format));
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
        protected void Write(Workbook workbook, DataTable table)
        {
            workbook.AddSheet(table);
        }

        /// <summary>
        /// Get File Format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        protected FileFormatType GetFileFormatType(ExcelFormat format)
        {
            switch (format)
            {
                case ExcelFormat.Xls:
                    return FileFormatType.Excel97To2003;

                case ExcelFormat.Xlsx:
                default:
                    return FileFormatType.Xlsx;
            }
        }

        /// <summary>
        /// Get Save Format
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        protected SaveFormat GetSaveFormat(ExcelFormat format) 
        {
            switch (format)
            {
                case ExcelFormat.Xls:
                    return SaveFormat.Excel97To2003;

                case ExcelFormat.Xlsx:
                default:
                    return SaveFormat.Xlsx;
            }
        }
    }
}
