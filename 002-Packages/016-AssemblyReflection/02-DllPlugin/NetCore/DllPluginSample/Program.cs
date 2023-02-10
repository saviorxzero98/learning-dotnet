using McMaster.NETCore.Plugins;
using MyPlugin;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DllPluginSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(typeof(JToken).Assembly.GetName().ToString());
            Console.WriteLine("");

            DoSample1();

            DoSample2();

            DoSample3();

            DoSample4();

            DoSample5();
        }

        /// <summary>
        /// 使用原生的方法讀取 DLL檔
        /// </summary>
        static void DoSample1()
        {
            Console.WriteLine("===== Demo Sample 01 =====");

            try
            {
                // Dll Path
                string filePath = @"Plugins\ExternalDll.dll";

                // Class Name
                string className = "SampleLoggerPlugin";

                // Load Dll File
                Assembly assembly = Assembly.LoadFrom(filePath);

                // Get AbstructLoggerPlugin Class Type
                Type type = assembly.GetTypes()
                                    .Where(t => typeof(AbstructLoggerPlugin).IsAssignableFrom(t) &&
                                                !t.IsAbstract &&
                                                t.Name == className)
                                    .FirstOrDefault();

                // Create Instance
                var logger = (AbstructLoggerPlugin)Activator.CreateInstance(type, "Info");

                // Invoke Method
                logger.Execute("Demo Sample 1");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// 使用原生的方法讀取 DLL檔
        /// </summary>
        static void DoSample2()
        {
            Console.WriteLine("===== Demo Sample 02 =====");

            try
            {
                // Dll Path
                string filePath = @"Plugins\ExternalDll.dll";

                // Class Name
                string classFullName = "ObjectLoggerPlugin";

                // Load Dll File
                Assembly assembly = Assembly.LoadFrom(filePath);

                // Get AbstructLoggerPlugin Class Type
                Type type = assembly.GetTypes()
                                    .Where(t => typeof(AbstructLoggerPlugin).IsAssignableFrom(t) &&
                                                !t.IsAbstract &&
                                                t.Name == classFullName)
                                    .FirstOrDefault();

                // Create Instance
                var logger = (AbstructLoggerPlugin)Activator.CreateInstance(type, "Info");

                // Invoke Method
                logger.Execute(new { Id = 2, Name = "Demo Sample 02" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// 使用 McMaster.NETCore.Plugins 讀取 DLL檔
        /// </summary>
        static void DoSample3()
        {
            Console.WriteLine("===== Demo Sample 03 =====");

            try
            {
                // Dll Path
                string filePath = Path.Combine(AppContext.BaseDirectory, @"Plugins\ExternalDll.dll");

                // Class Name
                string classFullName = "SampleLoggerPlugin";

                // Load Dll File
                var loader = PluginLoader.CreateFromAssemblyFile(assemblyFile: filePath,
                                                                 sharedTypes: new[] { typeof(AbstructLoggerPlugin) });

                // Get AbstructLoggerPlugin Class Type
                Type type = loader.LoadDefaultAssembly()
                                  .GetTypes()
                                  .Where(t => typeof(AbstructLoggerPlugin).IsAssignableFrom(t) && 
                                              !t.IsAbstract &&
                                              t.Name == classFullName)
                                  .FirstOrDefault();

                // Create Instance
                var logger = (AbstructLoggerPlugin)Activator.CreateInstance(type, "Info");

                // Invoke Method
                logger.Execute("Demo Sample 3");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// 使用 McMaster.NETCore.Plugins 讀取 DLL檔
        /// </summary>
        static void DoSample4()
        {
            Console.WriteLine("===== Demo Sample 04 =====");

            try
            {
                // Dll Path
                string filePath = Path.Combine(AppContext.BaseDirectory, @"Plugins\ExternalDll.dll");

                // Class Name
                string classFullName = "ObjectLoggerPlugin";

                // Load Dll File
                var loader = PluginLoader.CreateFromAssemblyFile(assemblyFile: filePath,
                                                                 sharedTypes: new[] { typeof(AbstructLoggerPlugin) });

                // Get AbstructLoggerPlugin Class Type
                Type type = loader.LoadDefaultAssembly()
                                  .GetTypes()
                                  .Where(t => typeof(AbstructLoggerPlugin).IsAssignableFrom(t) &&
                                              !t.IsAbstract &&
                                              t.Name == classFullName)
                                  .FirstOrDefault();

                // Create Instance
                var logger = (AbstructLoggerPlugin)Activator.CreateInstance(type, "Info");

                // Invoke Method
                logger.Execute(new { Id = 4, Name = "Demo Sample 04" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
        }

        /// <summary>
        /// 使用 McMaster.NETCore.Plugins 批次讀取 DLL檔
        /// </summary>
        static void DoSample5()
        {
            Console.WriteLine("===== Demo Sample 05 =====");

            try
            {
                var loaders = new List<PluginLoader>();

                // 取得 Plugin 目錄
                var pluginsDir = Path.Combine(AppContext.BaseDirectory, @"Plugins");
                var files = Directory.GetFiles(pluginsDir);
                foreach (var file in files)
                {
                    // 取得 Plugin 目錄底下的 DLL，建立 Plugin Loader
                    if (File.Exists(file))
                    {
                        var loader = PluginLoader.CreateFromAssemblyFile(assemblyFile: file,
                                                                         sharedTypes: new[] { typeof(AbstructLoggerPlugin) });
                        loaders.Add(loader);
                    }
                }

                var pluginList = new Dictionary<string, AbstructLoggerPlugin>();

                // 讀取 Plugin 目錄底下的 DLL
                foreach (var loader in loaders)
                {
                    var types = loader.LoadDefaultAssembly()
                                      .GetTypes()
                                      .Where(t => typeof(AbstructLoggerPlugin).IsAssignableFrom(t) && 
                                                  !t.IsAbstract);

                    foreach (var type in types)
                    {
                        var pluginName = type.Name;
                        var plugin = Activator.CreateInstance(type, "Info") as AbstructLoggerPlugin;

                        pluginList.Add(pluginName, plugin);
                    }
                }


                // 顯示載入的 Plugin
                var pluginNames = pluginList.Keys;

                Console.WriteLine("* Load Plugins Class：");
                foreach (var pluginName in pluginNames)
                {
                    Console.WriteLine(pluginName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
        }
    }
}
