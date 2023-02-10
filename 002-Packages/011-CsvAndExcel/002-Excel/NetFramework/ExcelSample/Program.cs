using ExcelSample.Core;
using ExcelSample.Data;
using System.Collections.Generic;
using System.IO;

namespace ExcelSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new List<Contact>
            {
                new Contact () { Name="Ram", Email="ram@techbrij.com", Phone="111-222-3333" },
                new Contact () { Name="Shyam", Email="shyam@techbrij.com", Phone="159-222-1596" },
                new Contact () { Name="Mohan", Email="mohan@techbrij.com", Phone="456-222-4569" },
                new Contact () { Name="Sohan", Email="sohan@techbrij.com", Phone="789-456-3333" },
                new Contact () { Name="Karan", Email="karan@techbrij.com", Phone="111-222-1234" },
                new Contact () { Name="Brij", Email="brij@techbrij.com", Phone="111-222-3333" }
            };

            var writer = new TableWriter();

            Directory.CreateDirectory("output");
            using (StreamWriter sw = new StreamWriter("output/CSV.csv"))
            {
                writer.WriteCsv(data, sw);
            }

            using (StreamWriter sw = new StreamWriter("output/TSV.tsv"))
            {
                writer.WriteTsv(data, sw);
            }

            using (StreamWriter sw = new StreamWriter("output/Table.html"))
            {
                writer.WriteHtmlTable(data, sw);
            }

            using (StreamWriter sw = new StreamWriter("output/Grid.html"))
            {
                writer.WriteHtmlGridView(data, sw);
            }
        }
    }
}
