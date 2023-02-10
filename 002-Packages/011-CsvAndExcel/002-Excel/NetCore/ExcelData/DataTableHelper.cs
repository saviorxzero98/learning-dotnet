using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ExcelSampleData
{
    public static class DataTableHelper
    {
        public static List<T> ToObjectList<T>(DataTable table)
        {
            if (table == null)
            {
                return new List<T>();
            }

            return table.Rows
                        .Cast<DataRow>()
                        .Select(row => ToObject<T>(row))
                        .ToList();
        }

        public static T ToObject<T>(DataRow row)
        {
            if (row == null)
            {
                return default(T);
            }

            var dict = row.Table
                          .Columns
                          .Cast<DataColumn>()
                          .ToDictionary(c => c.ColumnName, c => row[c]);
            return JObject.FromObject(dict).ToObject<T>();
        }

        public static DataTable ToDataTable<T>(List<T> items, string tableName)
        {
            DataTable dataTable = new DataTable(tableName);

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                var type = (prop.PropertyType.IsGenericType && 
                            prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            
            return dataTable;
        }
    }
}
