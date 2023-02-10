using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using TaiwanCalendarService.Models;

namespace TaiwanCalendarService.Toolkits
{
    public static class CalendarUtility
    {
        #region 日期時間

        /// <summary>
        /// 建立日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime Create(int year, int month, int day,
                                      int hour = 0, int minute = 0, int second = 0)
        {
            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// 尋找日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="weekOfMonth"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FindFirstDate(int year, int month, int weekOfMonth, DayOfWeek dayOfWeek,
                                        int hour = 0, int minute = 0, int second = 0)
        {
            if (weekOfMonth < 1 || weekOfMonth > 5)
            {
                return default(DateTime);
            }

            var firstDayOfMonth = new DateTime(year, month, 1);
            var daysNeeded = (int)dayOfWeek - (int)firstDayOfMonth.DayOfWeek;

            if (daysNeeded < 0)
            {
                daysNeeded = daysNeeded + 7;
            }

            var resultedDay = (daysNeeded + 1) + (7 * (weekOfMonth - 1));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return default(DateTime);
            }
            else
            {
                return new DateTime(year, month, resultedDay, hour, minute, second);
            }
        }

        /// <summary>
        /// 尋找日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="weekOfMonth"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FindLastDate(int year, int month, int weekOfMonth, DayOfWeek dayOfWeek,
                                        int hour = 0, int minute = 0, int second = 0)
        {
            if (weekOfMonth < 1 || weekOfMonth > 5)
            {
                return default(DateTime);
            }

            var daysInMonth = DateTime.DaysInMonth(year, month);
            var lastDayOfMonth = new DateTime(year, month, daysInMonth);
            var daysNeeded = (int)lastDayOfMonth.DayOfWeek - (int)dayOfWeek;

            if (daysNeeded < 0)
            {
                daysNeeded = daysNeeded + 7;
            }

            var resultedDay = daysInMonth - ((daysNeeded) + (7 * (weekOfMonth - 1)));

            if (resultedDay > DateTime.DaysInMonth(year, month))
            {
                return default(DateTime);
            }
            else
            {
                return new DateTime(year, month, resultedDay, hour, minute, second);
            }
        }

        /// <summary>
        /// Get First Day Of Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get Last Day Of Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfYear(DateTime date)
        {
            return FirstDayOfYear(date).AddYears(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Get First Day Of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1, 0, 0, 0);
        }

        /// <summary>
        /// Get Last Day Of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastDayOfMonth(DateTime date)
        {
            return FirstDayOfMonth(date).AddMonths(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Day Of Week
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstDayOfWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            return FirstTimeOfDay(date).AddDays((int)DayOfWeek.Sunday - (int)dayOfWeek);
        }

        /// <summary>
        /// Last Day Of Week
        /// </summary>
        public static DateTime LastDayOfWeek(DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;
            return LastTimeOfDay(date).AddDays((int)DayOfWeek.Saturday - (int)dayOfWeek);
        }

        /// <summary>
        /// First Millisecond Of Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime LastTimeOfDay(DateTime date)
        {
            return FirstTimeOfDay(date).AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfHour(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Hour
        /// </summary>
        public static DateTime LastTimeOfHour(DateTime date)
        {
            return FirstTimeOfHour(date).AddHours(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Minute
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfMinute(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0);
        }

        /// <summary>
        /// Last Millisecond Of Minute
        /// </summary>
        public static DateTime LastTimeOfMinute(DateTime date)
        {
            return FirstTimeOfMinute(date).AddMinutes(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// First Millisecond Of Second
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FirstTimeOfSecond(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
        }

        /// <summary>
        /// Last Millisecond Of Second
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime LastTimeOfSecond(DateTime date)
        {
            return FirstTimeOfSecond(date).AddSeconds(1).AddMilliseconds(-1);
        }

        #endregion


        #region 日期名稱

        /// <summary>
        /// Get Month Text
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetMonthString(DateTime date)
        {
            byte index = (byte)Convert.ToDateTime(date).Month;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var month = info.MonthNames[index];
            return month;
        }
        /// <summary>
        /// Get Month Text
        /// </summary>
        /// <param name="date"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetMonthString(DateTime date, string location)
        {
            byte index = (byte)Convert.ToDateTime(date).Month;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(new CultureInfo(location));
            var month = info.MonthNames[index];
            return month;
        }

        /// <summary>
        /// Get Week Text
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetDayOfWeekString(DateTime date)
        {
            byte index = (byte)Convert.ToDateTime(date).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(Thread.CurrentThread.CurrentCulture);
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }
        /// <summary>
        /// Get Week Text
        /// </summary>
        /// <param name="date"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetDayOfWeekString(DateTime date, string location)
        {
            byte index = (byte)Convert.ToDateTime(date).DayOfWeek;
            DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(new CultureInfo(location));
            var dayOfWeek = info.DayNames[index];
            return dayOfWeek;
        }

        #endregion


        #region 國曆與農曆轉換


        /// <summary>
        /// 國曆轉農曆
        /// </summary>
        /// <param name="solarDate"></param>
        /// <returns></returns>
        public static LunarDate ToLunarDate(DateTime solarDate)
        {
            var lunisolar = new ChineseLunisolarCalendar();

            int year = lunisolar.GetYear(solarDate);
            int month = lunisolar.GetMonth(solarDate);
            int day = lunisolar.GetDayOfMonth(solarDate);
            int leapMonth = lunisolar.GetLeapMonth(year);
            bool isLeapMonth = false;

            if (leapMonth > 0)
            {
                if (month == leapMonth)
                {
                    month = leapMonth - 1;
                    isLeapMonth = true;
                }

                if (month > leapMonth)
                {
                    month = month - 1;
                }
            }

            int hour = solarDate.Hour;
            int minute = solarDate.Minute;
            int second = solarDate.Second;

            return new LunarDate(year, month, day, isLeapMonth, hour, minute, second);
        }
        /// <summary>
        /// 國曆轉農曆
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static LunarDate ToLunarDate(int year, int month, int day,
                                            int hour = 0, int minute = 0, int second = 0)
        {
            DateTime solarDate = Create(year, month, day, hour, minute, second);
            return ToLunarDate(solarDate);
        }

        /// <summary>
        /// 農曆轉國曆
        /// </summary>
        /// <param name="lunarDate"></param>
        /// <returns></returns>
        public static DateTime FromLunarDate(LunarDate lunarDate)
        {
            var lunisolar = new ChineseLunisolarCalendar();
            int year = lunarDate.Year;
            int month = lunarDate.Month;
            int day = lunarDate.Day;
            int leapMonth = lunisolar.GetLeapMonth(year);

            if (lunarDate.IsLeapMonth)
            {
                month = leapMonth;
            }
            else
            {
                if (leapMonth != 0 && month >= leapMonth)
                {
                    month = month + 1;
                }
            }

            int hour = lunarDate.Hour;
            int minute = lunarDate.Minute;
            int second = lunarDate.Second;
            return new DateTime(year, month, day, hour, minute, second, lunisolar);
        }
        /// <summary>
        /// 農曆轉國曆
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="isLeapMonth"></param>
        /// <param name="hours"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FromLunarDate(int year, int month, int day, bool isLeapMonth = false, int hours = 0, int minute = 0, int second = 0)
        {
            var lunisolar = new ChineseLunisolarCalendar();
            int leapMonth = lunisolar.GetLeapMonth(year);

            if (isLeapMonth)
            {
                month = leapMonth;
            }
            else
            {
                if (leapMonth != 0 && month >= leapMonth)
                {
                    month = month + 1;
                }
            }
            return new DateTime(year, month, day, hours, minute, second, lunisolar);
        }


        #endregion


        #region 民國年

        private const string RocLocal = "zh-TW";

        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToRocDate(DateTime date)
        {
            if (date.Year < 1912)
            {
                return default(DateTime);
            }

            var tawianCalendar = new TaiwanCalendar();
            int year = tawianCalendar.GetYear(date);

            return new DateTime(year, date.Month, date.Day, date.Hour, date.Month, date.Second);
        }
        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime ToRocDate(int year, int month, int day,
                                         int hour = 0, int minute = 0, int second = 0)
        {
            return ToRocDate(new DateTime(year, month, day, hour, month, minute, second));
        }

        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime FromRocDate(DateTime date)
        {
            var tawianCalendar = new TaiwanCalendar();
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Month, date.Second, tawianCalendar);
        }
        /// <summary>
        /// 西元年轉民國年
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime FromRocDate(int year, int month, int day,
                                           int hour = 0, int minute = 0, int second = 0)
        {
            return FromRocDate(new DateTime(year, month, day, hour, month, minute, second));
        }

        /// <summary>
        /// 轉民國年字串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToRocDateString(this DateTime value, string format)
        {
            CultureInfo culture = new CultureInfo(RocLocal);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return value.ToString(format, culture);
        }

        /// <summary>
        /// 解析民國年字串
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static bool TryParseRocDate(string rocDateString, out DateTime date)
        {
            CultureInfo culture = new CultureInfo(RocLocal);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
        /// <summary>
        /// 解析民國年字串
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="format"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseRocDate(string rocDateString, string format, out DateTime date)
        {
            CultureInfo culture = new CultureInfo(RocLocal);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            if (DateTime.TryParseExact(rocDateString, format, culture, DateTimeStyles.None, out date))
            {
                return true;
            }

            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }
        /// <summary>
        /// 解析民國年字串
        /// </summary>
        /// <param name="rocDateString"></param>
        /// <param name="formats"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool TryParseRocDate(string rocDateString, IEnumerable<string> formats, out DateTime date)
        {
            CultureInfo culture = new CultureInfo(RocLocal);
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();

            if (formats != null && formats.ToArray().Length != 0)
            {
                if (DateTime.TryParseExact(rocDateString, formats.ToArray(), culture, DateTimeStyles.None, out date))
                {
                    return true;
                }
            }

            return DateTime.TryParse(rocDateString, culture, DateTimeStyles.None, out date);
        }

        #endregion
    }
}