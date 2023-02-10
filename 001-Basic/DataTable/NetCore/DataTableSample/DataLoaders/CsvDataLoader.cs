using CsvHelper;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;

namespace DataTableSample.DataLoaders
{
    public class CsvDataLoader
    {
        public DataTable Read(string fileName)
        {
            var data = new DataTable();

            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            using (var dataReader = new CsvDataReader(csv))
            {
                data.Load(dataReader);
            }
            return data;
        }


        /// <summary>
        /// 註冊編碼
        /// </summary>
        protected void RegisterEncodingProvider()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
