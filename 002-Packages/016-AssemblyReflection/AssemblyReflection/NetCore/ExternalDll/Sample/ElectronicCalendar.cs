using BasePlugin;
using System;

namespace ExternalDll.Sample
{
    // 實作Plugin
    public class ElectronicCalendar : IPlugin
    {
        public void Execute<T>(T param)
        {
            ShowDateTime(param.ToString());
        }

        public void ShowDateTime(string message = "")
        {
            if (message.Length == 0)
            {
                Console.WriteLine($"DateTime : {DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")}");
            }
            else
            {
                Console.WriteLine($"DateTime : {DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss")} ; Message : {message}");
            }
        }
    }
}
