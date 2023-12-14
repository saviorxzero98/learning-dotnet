using Npgg;
using SampleData;
using System.IO;

namespace NpoiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new NpoiExcelReader();
            var contacts = reader.Read<Contact>("Data/Sample.xlsx", "MyContact");

            ConsoleTable.Write(contacts);

            var writer = new NpoiExcelWriter();

            using (var stream = new FileStream("Data/NewSample.xlsx", FileMode.Create))
            {
                writer.Write(stream, contacts, "MyContact");
            }
        }
    }
}
