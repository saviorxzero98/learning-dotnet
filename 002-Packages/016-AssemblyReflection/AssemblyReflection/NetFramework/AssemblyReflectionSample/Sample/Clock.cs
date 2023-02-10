using System;

namespace AssemblyReflectionSample.Sample
{
    public class Clock
    {
        public void ShowNow(string message = "")
        {
            if(message.Length == 0)
            {
                Console.WriteLine($"Time : {DateTime.Now.ToString("HH:mm:ss")}");
            }
            else
            {
                Console.WriteLine($"Time : {DateTime.Now.ToString("HH:mm:ss")} ; Message : {message}");
            }
        }
    }
}
