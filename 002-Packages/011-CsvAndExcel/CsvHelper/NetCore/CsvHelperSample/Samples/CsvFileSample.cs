using CsvHelper;
using CsvHelperSample.Readers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CsvHelperSample.Samples
{
    public static class CsvFileSample
    {
        public static void Demo()
        {
            const string csvFile = "Data.csv";
            var printer = new TableConsolePrinter();
            var csvReader = new CsvFileReader();

            Console.WriteLine("===== CSV to Model =====");
            var result01 = csvReader.Read<WebSiteInfo>(csvFile);
            printer.Print(result01);

            Console.WriteLine("\n===== CSV to Model (指定 Header) =====");
            var result02 = ReadCsvToModelWithHeader(csvFile);
            printer.Print(result02);

            Console.WriteLine("\n===== CSV to DataTable =====");
            var result03 = csvReader.Read(csvFile);
            printer.Print(result03);

            Console.WriteLine("\n===== CSV to JSON =====");
            var result04 = csvReader.ReadCsvToJToken(csvFile);
            Console.WriteLine(result04.ToString());
        }

        /// <summary>
        /// CSV to Model (指定 Header)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static List<WebSiteInfo> ReadCsvToModelWithHeader(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<WebSiteInfo>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = new WebSiteInfo
                    {
                        Id = csv.GetField<int>(nameof(WebSiteInfo.Id)),
                        Name = csv.GetField(nameof(WebSiteInfo.Name)),
                        Url = csv.GetField(nameof(WebSiteInfo.Url))
                    };
                    records.Add(record);
                }

                return records;
            }
        }
    }
}
