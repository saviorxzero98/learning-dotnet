using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelSample.Core
{
    public class TableWriter
    {
        public void WriteTsv<T>(IEnumerable<T> data, TextWriter output, string separatedChar = "\t")
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write(prop.DisplayName); // header
                output.Write(separatedChar);
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write(prop.Converter.ConvertToString(prop.GetValue(item)));
                    output.Write(separatedChar);
                }
                output.WriteLine();
            }
        }

        public void WriteCsv<T>(IEnumerable<T> data, TextWriter output, string separatedChar = ",")
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in props)
            {
                output.Write("\"");
                output.Write(prop.DisplayName); // header
                output.Write("\"");
                output.Write(separatedChar);
            }
            output.WriteLine();
            foreach (T item in data)
            {
                foreach (PropertyDescriptor prop in props)
                {
                    output.Write("\"");
                    output.Write(prop.Converter.ConvertToString(prop.GetValue(item)));
                    output.Write("\"");
                    output.Write(separatedChar);
                }
                output.WriteLine();
            }
        }

        public void WriteHtmlTable<T>(IEnumerable<T> data, TextWriter output)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    //  Create a form to contain the List
                    Table table = new Table();
                    table.GridLines = GridLines.Both;
                    TableRow row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.Name;
                        row.Cells.Add(hcell);
                    }

                    table.Rows.Add(row);

                    //  add each of the data item to the table
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    output.Write(sw.ToString());
                }
            }
        }

        public void WriteHtmlGridView<T>(IEnumerable<T> data, TextWriter output)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    GridView grid = new GridView();
                    grid.DataSource = data;
                    grid.DataBind();
                    grid.RenderControl(htw);
                    output.Write(sw.ToString());
                }
            }
        }
    }
}
