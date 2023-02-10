using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;

namespace DataTableUtils.Extensions
{
    public static class DataTableExtensions
    {
        #region Add Column

        /// <summary>
        /// Add Column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataTable AddColumn<T>(this DataTable table, string name)
        {
            if (!table.Columns.Contains(name))
            {
                table.Columns.Add(name, typeof(T));
            }

            return table;
        }

        /// <summary>
        /// Add Column (with Default Value)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DataTable AddColumn<T>(this DataTable table, string name, T defaultValue)
        {
            if (!table.Columns.Contains(name))
            {
                table.Columns.Add(name, typeof(T));
                table.Columns[name].DefaultValue = defaultValue;
            }

            return table;
        }

        /// <summary>
        /// Add Column (with Expression)
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DataTable AddColumnWithExpression<T>(this DataTable table, string name, string expression)
        {
            if (!table.Columns.Contains(name))
            {
                table.Columns.Add(name, typeof(T), expression);
            }

            return table;
        }

        #endregion


        #region To List

        public static List<DataRow> ToList(this DataTable table)
        {
            List<DataRow> result = new List<DataRow>();

            foreach (var row in table.Rows)
            {
                result.Add((DataRow)row);
            }

            return result;
        }

        public static List<string> ToStringList(this DataTable table)
        {
            List<string> result = new List<string>();

            foreach (var row in table.Rows)
            {
                result.Add(ToJsonString((DataRow)row));
            }

            return result;
        }

        private static string ToJsonString(DataRow row)
        {
            var expando = new System.Dynamic.ExpandoObject() as IDictionary<string, object>;
            foreach (DataColumn col in row.Table.Columns)
            {
                expando[col.ColumnName] = row[col];
            }
            var json = JsonConvert.SerializeObject(expando);
            return json;
        }

        #endregion
    }
}
