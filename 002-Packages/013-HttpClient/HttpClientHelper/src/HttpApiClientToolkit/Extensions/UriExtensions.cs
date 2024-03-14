using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HttpApiClientToolkit.Extensions
{
    /// <summary>
    /// Uri 擴充函式
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Set Port
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static Uri SetPort(this Uri uri, int port)
        {
            var builder = new UriBuilder(uri);
            builder.Port = port;
            uri = builder.Uri;
            return uri;
        }

        /// <summary>
        /// Set Path
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Uri SetPath(this Uri uri, string path)
        {
            var builder = new UriBuilder(uri);
            builder.Path = path;
            uri = builder.Uri;
            return uri;
        }

        /// <summary>
        /// Set Query
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri SetQuery(this Uri uri, string key, string value)
        {
            var builder = new UriBuilder(uri);

            string queryString = string.Empty;

            if (!string.IsNullOrEmpty(key))
            {
                queryString += $"{key}={HttpUtility.UrlEncode(value)}";
            }

            builder.Query = queryString;

            uri = builder.Uri;
            return uri;
        }

        /// <summary>
        /// Set Query
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Uri SetQuery(this Uri uri, Dictionary<string, string> query)
        {
            var builder = new UriBuilder(uri);

            var queryString = new StringBuilder();
            var keys = new List<string>(query.Keys);

            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];

                if (i == 0)
                {
                    queryString.Append($"{key}={HttpUtility.UrlEncode(query[key])}");
                }
                else
                {
                    queryString.Append($"&{key}={HttpUtility.UrlEncode(query[key])}");
                }
            }
            builder.Query = queryString.ToString();

            uri = builder.Uri;
            return uri;
        }

        /// <summary>
        /// Get Query
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetQuery(this Uri uri)
        {
            var q = HttpUtility.ParseQueryString(uri.Query);
            var query = q.AllKeys.ToDictionary(k => k, k => q[k]) ?? new Dictionary<string, string>();
            return query;
        }

        /// <summary>
        /// Get Query
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string GetQuery(this Uri uri, string key, string defaultValue = "")
        {
            var query = uri.GetQuery();

            if (query.ContainsKey(key))
            {
                return query[key];
            }

            return defaultValue;
        }

        /// <summary>
        /// Add Query
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Uri AddQuery(this Uri uri, string key, string value)
        {
            var query = uri.GetQuery();
            query.Add(key, value);
            return uri.SetQuery(query);
        }

        /// <summary>
        /// Add Query
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static Uri AddQuery(this Uri uri, Dictionary<string, string> query)
        {
            var currentQuery = uri.GetQuery();

            var keys = new List<string>(query.Keys);

            foreach (var key in keys)
            {
                currentQuery.Add(key, query[key]);
            }

            return uri.SetQuery(currentQuery);
        }

        /// <summary>
        /// Remove Query
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Uri RemoveQuery(this Uri uri, string key)
        {
            var query = uri.GetQuery();

            if (query.ContainsKey(key))
            {
                query.Remove(key);
                return uri.SetQuery(query);
            }

            return uri;
        }
    }
}
