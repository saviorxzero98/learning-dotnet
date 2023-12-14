using Npgg;
using SampleData;

namespace ExcelDataReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var readers = new ExcelDataReader();

            var contacts = readers.Read<Contact>("Data/Sample.xlsx", "MyContact");

            ConsoleTable.Write(contacts);
        }
    }
}
