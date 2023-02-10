using System;
using System.Globalization;
using System.Threading;

namespace ExternalProject.Sample
{
    public class Calendar
    {
        public void ShowToday(string message = "")
        {
            if (message.Length == 0)
            {
                Console.WriteLine($"Date : {DateTime.Today.ToString("yyyy-MM-dd")}");
            }
            else
            {
                Console.WriteLine($"Date : {DateTime.Today.ToString("yyyy-MM-dd")} ; Message : {message}");
            }
        }

        public static int ShowTodayDayOfWeek(string message = "")
        {
            byte index = (byte)Convert.ToDateTime(DateTime.Today).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var dayOfWeek = info.DayNames[index];

            if (message.Length == 0)
            {
                Console.WriteLine($"Day of Week : {dayOfWeek}");
            }
            else
            {
                Console.WriteLine($"Day of Week : {dayOfWeek} ; Message : {message}");
            }
            return (int)DateTime.Today.DayOfWeek;
        }
    }
}
