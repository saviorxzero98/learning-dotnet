using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOISample.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace NPOISample
{
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
                IWorkbook workbook = GetWorkBook("xlsx");
                ISheet sheet = workbook.CreateSheet("My Contact");

                //make a header row
                IRow row1 = sheet.CreateRow(0);

                var props = typeof(Contact).GetProperties(BindingFlags.Public);
                for (int j = 0; j < props.Length; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    string columnName = props[j].ToString();
                    cell.SetCellValue(columnName);
                }

                //loops through data
                /*for (int i = 0; i < Contacts.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < props.Length; j++)
                    {

                        ICell cell = row.CreateCell(j);
                        string columnName = props[j].ToString();
                        cell.SetCellValue(Contacts.[columnName].ToString());
                    }
                }*/
            }
        }

        static IWorkbook GetWorkBook(string ext)
        {
            IWorkbook workbook;

            if (ext == "xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (ext == "xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                throw new Exception("This format is not supported");
            }
            return workbook;
        }

        static void ReadExcel()
        {

        }

        static DataTable DataTableToObjectList<T>(List<T> data)
        {
            string json = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<DataTable>(json);
        }

        static List<T> DataTableToObjectList<T>(DataTable table)
        {
            string json = JsonConvert.SerializeObject(table);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
