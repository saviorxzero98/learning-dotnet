using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using TaiwanCalendarService.Providers;

namespace Sample
{
    class Program
    {
        const int Year = 2023;

        static void Main(string[] args)
        {
            //DemoHoliday();
            ShowAllHoliday();
        }

        static void ShowAllHoliday()
        {
            var taiwanCalendar = new TaiwanCalendarProvider();

            var calendars = taiwanCalendar.GetAllCalendar(Year);

            foreach (var calendar in calendars)
            {
                DateTime date = calendar.DateTime;
                if (calendar.IsHoliday)
                {
                    if (string.IsNullOrEmpty(calendar.Name))
                    {
                        Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 放假\n");
                    }
                    else
                    {
                        Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 放假 ({calendar.Name})\n");
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(calendar.Name))
                    {
                        Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 不放假\n");
                    }
                    else
                    {
                        Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 不放假 ({calendar.Name})\n");
                    }
                }
            }
        }

        static void DemoHoliday()
        {
            //// 使用到台北市政府資料開放平台的資料
            //var taipeiCalendar = new TaipeiOpenCalendarProvider();
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            //Console.WriteLine("台北市");
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            //DemoCalendar(taipeiCalendar);

            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");

            //// 使用到新北市政府資料開放平台的資料
            //var newtaipeiCalendar = new NewTaipeiOpenCalendarProvider();
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            //Console.WriteLine("新北市");
            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            //DemoCalendar(newtaipeiCalendar);

            //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n");

            // 預設
            var defaultCalendar = new DefaultCalendarProvider();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("預設");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            DemoCalendar(defaultCalendar);

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            // 預設
            var taiwanCalendar = new TaiwanCalendarProvider();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("台灣節日");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            DemoTaiwanCalendar(taiwanCalendar);

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        static void DemoCalendar(ICalendarProvider provider)
        {
            // 1/1 New Year
            DateTime newYear = new DateTime(Year, 1, 1);
            Print(provider, newYear);

            // 1/2
            Print(provider, newYear.AddDays(1));

            // 1/3
            Print(provider, newYear.AddDays(2));

            // 1/7
            DateTime saturday = new DateTime(Year, 1, 7);
            Print(provider, saturday);

            // 1/8
            DateTime sunday = new DateTime(Year, 1, 8);
            Print(provider, sunday);

            // 1/29
            DateTime chineseNewYear = new DateTime(Year, 2, 14);
            Print(provider, chineseNewYear);

            // Labor Day
            DateTime laborDay = new DateTime(Year, 5, 1);
            Print(provider, laborDay);

            // 9/30
            DateTime workDay = new DateTime(Year, 9, 30);
            Print(provider, workDay);

            // 10/10 National Day
            DateTime nationalDay = new DateTime(Year, 10, 10);
            Print(provider, nationalDay);
        }

        static void Print(ICalendarProvider service, DateTime date, string note = "")
        {
            if (service.IsHoliday(date))
            {
                Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 放假日\n");
            }
            else
            {
                Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 上班日\n");
            }
        }

        static void DemoTaiwanCalendar(TaiwanCalendarProvider provider)
        {
            // 1/1 New Year
            DateTime newYear = new DateTime(Year, 1, 1);
            PrintTaiwanCalendar(provider, newYear);

            // 1/2
            PrintTaiwanCalendar(provider, newYear.AddDays(1));

            // 1/3
            PrintTaiwanCalendar(provider, newYear.AddDays(2));

            // 1/7
            DateTime saturday = new DateTime(Year, 1, 7);
            PrintTaiwanCalendar(provider, saturday);

            // 1/8
            DateTime sunday = new DateTime(Year, 1, 8);
            PrintTaiwanCalendar(provider, sunday);

            // 1/29
            DateTime chineseNewYear = new DateTime(Year, 2, 14);
            PrintTaiwanCalendar(provider, chineseNewYear);

            // Labor Day
            DateTime laborDay = new DateTime(Year, 5, 1);
            PrintTaiwanCalendar(provider, laborDay);

            // Monther Day
            DateTime montherDay = new DateTime(Year, 5, 10);
            PrintTaiwanCalendar(provider, montherDay);

            // 9/30
            DateTime workDay = new DateTime(Year, 9, 30);
            PrintTaiwanCalendar(provider, workDay);

            // 10/10 National Day
            DateTime nationalDay = new DateTime(Year, 10, 10);
            PrintTaiwanCalendar(provider, nationalDay);
        }

        static void PrintTaiwanCalendar(TaiwanCalendarProvider service, DateTime date, string note = "")
        {
            var calendars = service.GetCalendars(date);

            if (service.IsHoliday(date))
            {
                var nameList = calendars.Where(c => !string.IsNullOrEmpty(c.Name))
                                        .Select(c => c.Name)
                                        .ToList();

                if (!nameList.Any())
                {
                    Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 放假日\n");
                }
                else
                {
                    string names = string.Join("、", nameList);
                    Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 放假日 ({names})\n");
                }
                
            }
            else
            {
                var nameList = calendars.Where(c => !string.IsNullOrEmpty(c.Name))
                                        .Select(c => c.Name)
                                        .ToList();

                if (!nameList.Any())
                {
                    Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 上班日\n");
                }
                else
                {
                    string names = string.Join("、", nameList);
                    Console.WriteLine($"{date.ToString("yyyy年MM月dd日")} {PrintDateofWeek(date)} : 上班日 ({names})\n");
                } 
            }
        }


        static string PrintDateofWeek(DateTime date)
        {
            byte index = (byte)Convert.ToDateTime(date).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }
    }
}
