using Npgg;
using SampleData;

namespace ClosedXMLSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var readers = new ClosedXmlExcelReader();

            var contacts = readers.Read<Contact>("Data/Sample.xlsx", "MyContact");

            ConsoleTable.Write(contacts);

            var writer = new ClosedXmlExcelWriter();

            using (var stream = new FileStream("Data/NewSample.xlsx", FileMode.Create))
            {
                writer.Write(stream, contacts, "MyContact");
            }
        }
    }
}
