using ConnectionStringTool;
using Microsoft.Extensions.Configuration;
using System;

namespace ConfigurationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // [方法一] 使用 App.config
            UseAppConfig();

            // [方法二] 使用 appsettings.json
            UseAppSettingJson();

            // [方法三] 使用 appsettings.ini
            UseAppSettingIni();

            // [方法四] 使用 appsettings.xml
            UseAppSettingXml();

            // [方法五] 使用 appsettings.yaml
            UseAppSettingYaml();

            // 使用密碼加密的連線字串
            UseEncryptConnectionStirng();
        }

        /// <summary>
        /// [方法一] 使用 App.config
        /// </summary>
        static void UseAppConfig()
        {
            Console.WriteLine($"===== app.config =====");

            // (a) 取得 App Settings
            var mySetting = System.Configuration.ConfigurationManager.AppSettings.Get("MySetting");

            // (b) 取得資料庫連線字串
            var myConnection = System.Configuration.ConfigurationManager.ConnectionStrings["MY_DB"].ToString();

            Console.WriteLine($"App.config AppSetting : {mySetting}");
            Console.WriteLine($"App.config ConnectionString : {myConnection}");
        }

        /// <summary>
        /// [方法二] 使用 appsettings.json
        /// </summary>
        static void UseAppSettingJson()
        {
            Console.WriteLine($"\n===== appsettings.json =====");

            // 1.建立 IConfiguration
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", 
                                                                           optional: true, 
                                                                           reloadOnChange: true)
                                                              .Build();

            // 2. 透過 IConfiguration 存取
            // (a) 設定資料自動 Bind 到 物件
            MySetting mySetting = config.GetSection("MySetting").Get<MySetting>();

            // (b) 直接取得設定資料值
            string myPhone = config.GetSection("MySetting:Phone").Value;

            // (c) 取得資料庫連線字串
            string myConnection = config.GetConnectionString("Default");

            Console.WriteLine($"AppSetting : {mySetting}");
            Console.WriteLine($"ConnectionString : {myConnection}");
        }

        /// <summary>
        /// [方法三] 使用 appsettings.ini
        /// </summary>
        static void UseAppSettingIni()
        {
            Console.WriteLine($"\n===== appsettings.ini =====");

            // 1.建立 IConfiguration
            IConfiguration config = new ConfigurationBuilder().AddIniFile("appsettings.ini",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build();

            // 2. 透過 IConfiguration 存取
            // (a) 設定資料自動 Bind 到 物件
            MySetting mySetting = config.GetSection("MySetting").Get<MySetting>();

            // (b) 直接取得設定資料值
            string myPhone = config.GetSection("MySetting:Phone").Value;

            // (c) 取得資料庫連線字串
            string myConnection = config.GetConnectionString("Default");

            Console.WriteLine($"AppSetting : {mySetting}");
            Console.WriteLine($"ConnectionString : {myConnection}");
        }

        /// <summary>
        /// [方法四] 使用 appsettings.xml
        /// </summary>
        static void UseAppSettingXml()
        {
            Console.WriteLine($"\n===== appsettings.xml =====");

            // 1.建立 IConfiguration
            IConfiguration config = new ConfigurationBuilder().AddXmlFile("appsettings.xml",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build();

            // 2. 透過 IConfiguration 存取
            // (a) 設定資料自動 Bind 到 物件
            MySetting mySetting = config.GetSection("MySetting").Get<MySetting>();

            // (b) 直接取得設定資料值
            string myPhone = config.GetSection("MySetting:Phone").Value;

            // (c) 取得資料庫連線字串
            string myConnection = config.GetConnectionString("Default");

            Console.WriteLine($"AppSetting : {mySetting}");
            Console.WriteLine($"ConnectionString : {myConnection}");
        }

        /// <summary>
        /// [方法五] 使用 appsettings.yaml
        /// </summary>
        static void UseAppSettingYaml()
        {
            Console.WriteLine($"\n===== appsettings.yaml =====");

            // 1.建立 IConfiguration
            IConfiguration config = new ConfigurationBuilder().AddYamlFile("appsettings.yaml",
                                                                            optional: true,
                                                                            reloadOnChange: true)
                                                              .Build();

            // 2. 透過 IConfiguration 存取
            // (a) 設定資料自動 Bind 到 物件
            MySetting mySetting = config.GetSection("MySetting").Get<MySetting>();

            // (b) 直接取得設定資料值
            string myPhone = config.GetSection("MySetting:Phone").Value;

            // (c) 取得資料庫連線字串
            string myConnection = config.GetConnectionString("Default");

            Console.WriteLine($"AppSetting : {mySetting}");
            Console.WriteLine($"ConnectionString : {myConnection}");
        }

        /// <summary>
        /// 使用密碼加密的連線字串
        /// </summary>
        static void UseEncryptConnectionStirng()
        {
            Console.WriteLine($"\n===== Encrypt Connection Stirng =====");

            // 1.建立 IConfiguration
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json",
                                                                           optional: true,
                                                                           reloadOnChange: true)
                                                              .Build();

            // 2. 透過 IConfiguration 存取
            string sqlServerConnection = config.GetConnectionString("SqlServer");
            sqlServerConnection = ConnectionStringHelper.GetSecretConnectionString(sqlServerConnection);

            string postgreServerConnection = config.GetConnectionString("PostgreSql");
            postgreServerConnection = ConnectionStringHelper.GetSecretConnectionString(postgreServerConnection);

            Console.WriteLine($"SQL Server : {sqlServerConnection}");
            Console.WriteLine($"PostgreSQL : {postgreServerConnection}");
        }

    }
}
