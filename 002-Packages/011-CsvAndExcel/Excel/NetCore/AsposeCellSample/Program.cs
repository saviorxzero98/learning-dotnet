using Npgg;
using SampleData;

namespace AsposeCellSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new AsposeCellExcelReader();
            var contacts = reader.Read<Contact>("Data/Sample.xlsx", "MyContact");

            ConsoleTable.Write(contacts);

            var writer = new AsposeCellExcelWriter();

            using (var stream = new FileStream("Data/NewSample.xlsx", FileMode.Create))
            {
                writer.Write(stream, contacts, "MyContact");
            }
        }
    }
}
