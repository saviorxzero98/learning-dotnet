using EPPlusSample.Data;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace EPPlusSample
{
    /// <summary>
    /// Read / Write Excel
    /// </summary>
    class Program
    {
        static List<Contact> Contacts;

        static void Main(string[] args)
        {
            Contacts = ContactData.ContactList;

            WriteExcel();

            ReadExcel();
        }

        static void WriteExcel()
        {
            using (FileStream fs = new FileStream("Sample.xlsx", FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("My Contact");
                workSheet.Cells[1, 1].LoadFromCollection(Contacts, true);
                workSheet.Column(5).Style.Numberformat.Format = "yyyy-mm-dd hh:mm";

                for (int i=1; i<=5; i++)
                {
                    workSheet.Column(i).AutoFit();
                }
                excel.SaveAs(fs);
            }
        }

        static List<Contact> ReadExcel()
        {
            using (FileStream fs = new FileStream("Sample.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                ExcelPackage excel = new ExcelPackage(fs);

                ExcelWorksheet sheet = excel.Workbook.Worksheets[1];

                var data = ExcelSheetToDataTable(sheet);
                var contacts = DataTableToObjectList<Contact>(data);
                return contacts;
            }
        }

        static DataTable ExcelSheetToDataTable(ExcelWorksheet sheet, bool hasHeader = true)
        {
            DataTable table = new DataTable();
            int startRowNumber = sheet.Dimension.Start.Row;
            int endRowNumber = sheet.Dimension.End.Row;
            int startColumn = sheet.Dimension.Start.Column;
            int endColumn = sheet.Dimension.End.Column;

            // Add Header
            foreach (var firstRowCell in sheet.Cells[1, 1, 1, endColumn])
            {
                table.Columns.Add(hasHeader ? firstRowCell.Text : $"Column {firstRowCell.Start.Column}");
            }

            if (hasHeader)
            {
                startRowNumber += 1;
            }

            for (int rowNum = startRowNumber; rowNum <= endRowNumber; rowNum++)
            {
                var wsRow = sheet.Cells[rowNum, 1, rowNum, endColumn];
                DataRow row = table.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }

            return table;
        }

        static List<T> DataTableToObjectList<T>(DataTable table)
        {
            string json = JsonConvert.SerializeObject(table);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
