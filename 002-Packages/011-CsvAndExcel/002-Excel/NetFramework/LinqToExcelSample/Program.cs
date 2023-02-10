using LinqToExcel;
using LinqToExcelSample.Data;
using System.Collections.Generic;

namespace LinqToExcelSample
{
    /// <summary>
    /// Read Excel
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var data = ReadExcel();
        }

        static List<Contact> ReadExcel()
        {
            ExcelQueryFactory excel = new ExcelQueryFactory("Sample.xlsx");
            var data = new List<Contact>(excel.Worksheet<Contact>("My Contact"));
            return data;
        }

    }
}
