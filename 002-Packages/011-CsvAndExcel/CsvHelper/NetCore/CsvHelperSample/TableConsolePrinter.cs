using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data;

namespace CsvHelperSample
{
    public class TableConsolePrinter
    {
        /// <summary>
        /// Print Table
        /// </summary>
        /// <param name="data"></param>
        public void Print<T>(IEnumerable<T> data)
        {
            var table = ConsoleTable.From(data);
            table.Configure(o => o.EnableCount = false)
                 .Configure(o => o.NumberAlignment = Alignment.Right)
                 .Write();
            Console.WriteLine();
        }

        /// <summary>
        /// Print Table
        /// </summary>
        /// <param name="dataTable"></param>
        public void Print(DataTable dataTable)
        {
            List<string> headers = new List<string>();
            foreach (DataColumn column in dataTable.Columns)
            {
                headers.Add(column.ColumnName);
            }

            var table = new ConsoleTable(headers.ToArray());

            for (int r = 0; r < dataTable.Rows.Count; r++)
            {
                List<object> rowValues = new List<object>();

                for (int c = 0; c < dataTable.Columns.Count; c++)
                {
                    rowValues.Add(dataTable.Rows[r][c]);
                }

                table.AddRow(rowValues.ToArray());
            }

            table.Configure(o => o.EnableCount = false)
                 .Configure(o => o.NumberAlignment = Alignment.Right)
                 .Write();
            Console.WriteLine();
        }
    }
}
