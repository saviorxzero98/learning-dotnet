using System;
using System.Data.Common;

namespace ConnectionStringTool
{
    public static class ConnectionStringHelper
    {
        public static string GetSecretConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return connectionString;
            }

            try
            {
                var builder = new DbConnectionStringBuilder();
                builder.ConnectionString = connectionString;

                var isEncrypt = Convert.ToBoolean(builder["Encrypt"]);
                var secretText = Convert.ToString(builder["Password"]);

                if (isEncrypt && !string.IsNullOrEmpty(secretText))
                {
                    builder["Password"] = ConnectionStringCypto.Decrypt(secretText);
                    builder.Remove("Encrypt");

                    string connect = builder.ConnectionString;
                    return connect;
                }
            }
            catch
            {
                
            }

            return connectionString;
        }
    }
}
