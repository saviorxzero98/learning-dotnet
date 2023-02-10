using AssemblyReflectionSample.Sample;
using BasePlugin;
using ExternalProject.Sample;
using System;
using System.Linq;
using System.Reflection;

namespace AssemblyReflectionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            DoSample1();

            DoSample2();

            DoSample3();

            DoSample4();

            DoSample5();
        }

        /// <summary>
        /// Demo Sample 1 - 透過string (Class Full Name)建立已經存在之物件
        /// </summary>
        static void DoSample1()
        {
            // Namespace + Class Name
            string classFullName = "AssemblyReflectionSample.Sample.Clock";

            Type type = Type.GetType(classFullName);
            var clock = Activator.CreateInstance(type) as Clock;
            clock.ShowNow("Demo Sample 1");
        }

        /// <summary>
        /// Demo Sample 2 - 透過string (Class Full Name)建立已經存在之物件(外部參考之專案)
        /// </summary>
        static void DoSample2()
        {
            // Namespace + Class Name
            string classFullName = "ExternalProject.Sample.Calendar";

            var type = Assembly.GetExecutingAssembly()
                               .GetReferencedAssemblies()
                               .Select(x => Assembly.Load(x))
                               .SelectMany(x => x.GetTypes())
                               .First(x => x.FullName == classFullName);
            var calendar = Activator.CreateInstance(type) as Calendar;
            calendar.ShowToday("Demo Sample 2");
        }

        /// <summary>
        /// Demo Sample 3 - 讀入Dll檔，並且執行Dll裡的Method
        /// </summary>
        static void DoSample3()
        {
            // Dll Path
            string filePath = @"Plugins\ExternalProject.dll";
            // Namespace + Class Name
            string classFullName = "ExternalProject.Sample.Calendar";


            Assembly assembly = Assembly.LoadFrom(filePath);
            Type type = assembly.GetType(classFullName);

            dynamic calendar = Activator.CreateInstance(type);
            calendar.ShowToday("Demo Sample 3");
        }

        /// <summary>
        /// Demo Sample 4 - 讀入Dll檔，並且執行Dll裡的Static Method
        /// </summary>
        static void DoSample4()
        {
            // Dll Path
            string filePath = @"Plugins\ExternalProject.dll";
            // Namespace + Class Name
            string classFullName = "ExternalProject.Sample.Calendar";
            // Method Name
            string methodName = "ShowTodayDayOfWeek";

            Assembly assembly = Assembly.LoadFrom(filePath);
            Type type = assembly.GetType(classFullName);
            MethodInfo method = type.GetMethod(methodName);
            object calendar = Activator.CreateInstance(type);

            // 第二個參數為Method Arguments
            var result = method.Invoke(calendar, new string[] { "Demo Sample 4" });
        }

        /// <summary>
        /// Demo Sample 5 - 延伸Sample 3，實作簡單的Plugin結構
        /// </summary>
        static void DoSample5()
        {
            // Dll Path
            string filePath = @"Plugins\ExternalDll.dll";
            // Namespace + Class Name
            string classFullName = "ExternalDll.Sample.ElectronicCalendar";

            Assembly assembly = Assembly.LoadFrom(filePath);
            Type type = assembly.GetType(classFullName);

            var calendar = (IPlugin)Activator.CreateInstance(type);
            calendar.Execute("Demo Sample 5");
        }
    }
}
