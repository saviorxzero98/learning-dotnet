using CsvHelper;
using CsvHelperSample.Readers;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;

namespace CsvHelperSample.Samples
{
    public static class ExcelFileSample
    {
        public static void Demo()
        {
            const string xlsxFile = "Data.xlsx";
            const string SheetName = "Data";
            var printer = new TableConsolePrinter();
            var xlsxReader = new ExcelFileReader();

            Console.WriteLine("===== Excel to Model =====");
            var result01 = xlsxReader.Read<WebSiteInfo>(xlsxFile, SheetName);
            printer.Print(result01);

            Console.WriteLine("\n===== Excel to DataTable =====");
            var result03 = xlsxReader.Read(xlsxFile, SheetName);
            printer.Print(result03);

            Console.WriteLine("\n===== Excel to JSON =====");
            var result04 = xlsxReader.ReadExcelToJToken(xlsxFile, SheetName);
            Console.WriteLine(result04.ToString());
        }
    }
}
