using System.Data;

namespace ExcelLibSampleCommon
{
    public interface IExcelReader
    {
        /// <summary>
        /// 讀取 Excel
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        DataSet Read(string filePath);


        /// <summary>
        /// 讀取 Excel (指定 Sheet)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        DataTable Read(string filePath, string sheetName);

        /// <summary>
        /// 讀取 Excel (指定 Sheet)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        List<T> Read<T>(string filePath, string sheetName) where T : class;
    }
}
