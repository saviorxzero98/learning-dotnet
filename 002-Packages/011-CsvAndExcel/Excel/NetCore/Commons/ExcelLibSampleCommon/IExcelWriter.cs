using System.Data;

namespace ExcelLibSampleCommon
{
    public interface IExcelWriter
    {
        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        void Write(Stream stream, DataSet data, ExcelFormat format);

        /// <summary>
        /// 寫入
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="format"></param>
        void Write(Stream stream, DataTable data, ExcelFormat format);

        /// <summary>
        /// 寫入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="data"></param>
        /// <param name="sheetName"></param>
        /// <param name="format"></param>
        void Write<T>(Stream stream, List<T> data, string sheetName, ExcelFormat format);
    }
}
