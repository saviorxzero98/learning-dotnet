using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace EvaluateExpressionSample.Scripts
{
    public static class Date
    {
        #region Get DateTIme

        /// <summary>
        /// Parse Date Format
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static DateTime Parse(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return dateTime;
        }
        /// <summary>
        /// Parse Date Format
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime Parse(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return dateTime;
        }
        /// <summary>
        /// Parse Date Format
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime Parse(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return dateTime;
        }


        /// <summary>
        /// Now
        /// </summary>
        /// <returns></returns>
        public static DateTime Now()
        {
            return DateTime.Now;
        }
        

        /// <summary>
        /// Today
        /// </summary>
        /// <returns></returns>
        public static DateTime Today()
        {
            DateTime today = DateTime.Today;
            return today;
        }
        /// <summary>
        /// Today
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static DateTime Today(int hour, int minute)
        {
            return Today().AddHours(hour)
                          .AddMinutes(minute);
        }
        /// <summary>
        /// Today
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime Today(int hour, int minute, int second)
        {
            return Today(hour, minute).AddSeconds(second);
        }


        /// <summary>
        /// Yesterday
        /// </summary>
        /// <returns></returns>
        public static DateTime Yesterday()
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            return yesterday;
        }
        /// <summary>
        /// Yesterday
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static DateTime Yesterday(int hour, int minute)
        {
            return Yesterday().AddHours(hour)
                              .AddMinutes(minute);
        }
        /// <summary>
        /// Yesterday
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime Yesterday(int hour, int minute, int second)
        {
            return Yesterday(hour, minute).AddSeconds(second);
        }


        /// <summary>
        /// Tomorrow
        /// </summary>
        /// <returns></returns>
        public static DateTime Tomorrow()
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            return tomorrow;
        }
        /// <summary>
        /// Tomorrow
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static DateTime Tomorrow(int hour, int minute)
        {
            return Tomorrow().AddHours(hour)
                             .AddMinutes(minute);
        }
        /// <summary>
        /// Tomorrow
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime Tomorrow(int hour, int minute, int second)
        {
            return Tomorrow(hour, minute).AddSeconds(second);
        }

        /// <summary>
        /// Utc Now
        /// </summary>
        /// <returns></returns>
        public static DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }


        /// <summary>
        ///  Get Date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime GetDate(int year, int month, int day)
        {
            return new DateTime(year, month, day);
        }
        /// <summary>
        /// Get Date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public static DateTime GetDate(int year, int month, int day, int hour, int minute)
        {
            return new DateTime(year, month, day, hour, minute, 0);
        }
        /// <summary>
        ///  Get Date
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static DateTime GetDate(int year, int month, int day, int hour, int minute, int second)
        {
            return new DateTime(year, month, day, hour, minute, second);
        }

        #endregion


        #region Get Date or Time

        /// <summary>
        /// Get Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int Year(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? Year(dateTime) : 0;
        }
        /// <summary>
        /// Get Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Year(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? Year(dateTime) : 0;
        }
        /// <summary>
        /// Get Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int Year(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? Year(dateTime) : 0;
        }
        /// <summary>
        /// Get Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Year(DateTime date)
        {
            return date.Year;
        }


        /// <summary>
        /// Get Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int Month(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? Month(dateTime) : 0;
        }
        /// <summary>
        /// Get Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Month(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? Month(dateTime) : 0;
        }
        /// <summary>
        /// Get Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int Month(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? Month(dateTime) : 0;
        }
        /// <summary>
        /// Get Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Month(DateTime date)
        {
            return date.Month;
        }


        /// <summary>
        /// Get Day
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int Day(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? Day(dateTime) : 0;
        }
        /// <summary>
        /// Get Day
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Day(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? Day(dateTime) : 0;
        }
        /// <summary>
        /// Get Day
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int Day(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? Day(dateTime) : 0;
        }
        /// <summary>
        /// Get Day
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Day(DateTime date)
        {
            return date.Day;
        }


        /// <summary>
        /// Get Hour
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int Hour(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? Hour(dateTime) : 0;
        }
        /// <summary>
        /// Get Hour
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Hour(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? Hour(dateTime) : 0;
        }
        /// <summary>
        /// Get Hour
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int Hour(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? Hour(dateTime) : 0;
        }
        /// <summary>
        /// Get Hour
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Hour(DateTime date)
        {
            return date.Hour;
        }

        /// <summary>
        /// Get Minute
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Minute(string dateText, string format = "")
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? Minute(dateTime) : 0;
        }
        /// <summary>
        /// Get Minute
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int Minute(string dateText, IEnumerable<string> formats = null)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? Minute(dateTime) : 0;
        }
        /// <summary>
        /// Get Minute
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Minute(DateTime date)
        {
            return date.Minute;
        }


        /// <summary>
        /// Get Minute
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int Minute(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? Minute(dateTime) : 0;
        }
        /// <summary>
        /// Get Second
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int Second(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? Second(dateTime) : 0;
        }
        /// <summary>
        /// Get Second
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int Second(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? Second(dateTime) : 0;
        }
        /// <summary>
        /// Get Second
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int Second(DateTime date)
        {
            return date.Second;
        }


        /// <summary>
        /// Get Week Of Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int WeekOfYear(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? WeekOfYear(dateTime) : 0;
        }
        /// <summary>
        /// Get Week Of Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int WeekOfYear(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? WeekOfYear(dateTime) : 0;
        }
        /// <summary>
        /// Get Week Of Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int WeekOfYear(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? WeekOfYear(dateTime) : 0;
        }
        /// <summary>
        /// Get Week Of Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekOfYear(DateTime date)
        {
            CultureInfo curr = CultureInfo.CurrentCulture;
            int week = curr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);
            return week;
        }


        /// <summary>
        /// Get Week Of Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int WeekOfMonth(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? WeekOfMonth(dateTime) : 0;
        }
        /// <summary>
        /// Get Week Of Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int WeekOfMonth(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? WeekOfMonth(dateTime) : 0;
        }
        /// <summary>
        /// Get Week Of Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int WeekOfMonth(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? WeekOfMonth(dateTime) : 0;
        }
        /// <summary>
        /// Get Week Of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int WeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);
            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
            {
                date = date.AddDays(1);
            }
            return (int)Math.Truncate(date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }


        /// <summary>
        /// Get Day Of Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int DayOfYear(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? DayOfYear(dateTime) : 0;
        }
        /// <summary>
        /// Get Day Of Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int DayOfYear(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? DayOfYear(dateTime) : 0;
        }
        /// <summary>
        /// Get Day Of Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int DayOfYear(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? DayOfYear(dateTime) : 0;
        }
        /// <summary>
        /// Get Day Of Year
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DayOfYear(DateTime date)
        {
            return date.DayOfYear;
        }


        /// <summary>
        /// Get Day Of Week
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int DayOfWeek(string dateText)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? DayOfWeek(dateTime) : 0;
        }
        /// <summary>
        /// Get Day Of Week
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int DayOfWeek(string dateText, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? DayOfWeek(dateTime) : 0;
        }
        /// <summary>
        /// Get Day Of Week
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int DayOfWeek(string dateText, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? DayOfWeek(dateTime) : 0;
        }
        /// <summary>
        /// Get Day Of Week
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DayOfWeek(DateTime date)
        {
            return (int)date.DayOfWeek;
        }


        /// <summary>
        /// Get Day Of Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <returns></returns>
        public static int DayOfMonth(string dateText)
        {
            return Day(dateText);
        }
        /// <summary>
        /// Get Day Of Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static int DayOfMonth(string dateText, string format)
        {
            return Day(dateText, format);
        }
        /// <summary>
        /// Get Day Of Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static int DayOfMonth(string dateText, IEnumerable<string> formats)
        {
            return Day(dateText, formats);
        }
        /// <summary>
        /// Get Day Of Month
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int DayOfMonth(DateTime date)
        {
            return date.Day;
        }

        #endregion


        #region To String

        /// <summary>
        /// Format
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="toFormat"></param>
        /// <returns></returns>
        public static string Format(string dateText, string toFormat)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);

            if (isSuccess)
            {
                return dateTime.ToString(toFormat);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Format
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="toFormat"></param>
        /// <param name="fromFormat"></param>
        /// <returns></returns>
        public static string Format(string dateText, string toFormat, string fromFormat)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, fromFormat);

            if (isSuccess)
            {
                return dateTime.ToString(toFormat);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Format
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="toFormat"></param>
        /// <param name="fromFormats"></param>
        /// <returns></returns>
        public static string Format(string dateText, string toFormat, IEnumerable<string> fromFormats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, fromFormats);

            if (isSuccess)
            {
                return dateTime.ToString(toFormat);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Format
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="toFormat"></param>
        /// <returns></returns>
        public static string Format(DateTime dateTime, string toFormat)
        {
            return dateTime.ToString(toFormat);
        }

        #endregion


        #region Calculate

        /// <summary>
        /// Add Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddYears(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddYears(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddYears(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddYears(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Year
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddYears(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddYears(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Year
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddYears(DateTime date, int value)
        {
            return date.AddYears(value);
        }


        /// <summary>
        /// Add Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddMonths(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddMonths(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddMonths(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddMonths(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Month
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddMonths(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddMonths(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Month
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddMonths(DateTime date, int value)
        {
            return date.AddMonths(value);
        }


        /// <summary>
        /// Add Week
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddWeeks(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddWeeks(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Week
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddWeeks(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddWeeks(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Week
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddWeeks(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddWeeks(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Week
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddWeeks(DateTime date, int value)
        {
            return date.AddDays(value * 7);
        }


        /// <summary>
        /// Add Day
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddDays(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddDays(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Day
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddDays(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddDays(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Day
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddDays(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddDays(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Day
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddDays(DateTime date, int value)
        {
            return date.AddDays(value);
        }


        /// <summary>
        /// Add Hour
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddHours(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddHours(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Hour
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddHours(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddHours(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Hour
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddHours(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddHours(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Hour
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddHours(DateTime date, int value)
        {
            return date.AddHours(value);
        }


        /// <summary>
        /// Add Minute
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddMinutes(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddMinutes(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Minute
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddMinutes(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddMinutes(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Minute
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddMinutes(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddMinutes(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Minute
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddMinutes(DateTime date, int value)
        {
            return date.AddMinutes(value);
        }


        /// <summary>
        /// Add Second
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddSeconds(string dateText, int value)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, string.Empty);
            return (isSuccess) ? AddSeconds(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Second
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime AddSeconds(string dateText, int value, string format)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, format);
            return (isSuccess) ? AddSeconds(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Second
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="value"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static DateTime AddSeconds(string dateText, int value, IEnumerable<string> formats)
        {
            (bool isSuccess, DateTime dateTime) = ToDateTime(dateText, formats);
            return (isSuccess) ? AddSeconds(dateTime, value) : new DateTime();
        }
        /// <summary>
        /// Add Second
        /// </summary>
        /// <param name="date"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime AddSeconds(DateTime date, int value)
        {
            return date.AddSeconds(value);
        }


        /// <summary>
        /// TimeSpan
        /// </summary>
        /// <param name="currentDateText"></param>
        /// <param name="targetDateText"></param>
        /// <param name="timeUnit"></param>
        /// <returns></returns>
        public static double TimeSpan(string currentDateText, string targetDateText, string timeUnit)
        {
            (bool isCurrentSuccess, DateTime currentDate) = ToDateTime(currentDateText, string.Empty);
            (bool isTargetSuccess, DateTime targetDate) = ToDateTime(targetDateText, string.Empty);

            if (isCurrentSuccess && isTargetSuccess)
            {
                return TimeSpan(currentDate, targetDate, timeUnit);
            }
            return 0;
        }
        /// <summary>
        /// TimeSpan
        /// </summary>
        /// <param name="currentDateText"></param>
        /// <param name="targetDateText"></param>
        /// <param name="timeUnit"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double TimeSpan(string currentDateText, string targetDateText, string timeUnit, string format)
        {
            (bool isCurrentSuccess, DateTime currentDate) = ToDateTime(currentDateText, format);
            (bool isTargetSuccess, DateTime targetDate) = ToDateTime(targetDateText, format);

            if (isCurrentSuccess && isTargetSuccess)
            {
                return TimeSpan(currentDate, targetDate, timeUnit);
            }
            return 0;
        }
        /// <summary>
        /// TimeSpan
        /// </summary>
        /// <param name="currentDateText"></param>
        /// <param name="targetDateText"></param>
        /// <param name="timeUnit"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        public static double TimeSpan(string currentDateText, string targetDateText, string timeUnit, IEnumerable<string> formats)
        {
            (bool isCurrentSuccess, DateTime currentDate) = ToDateTime(currentDateText, formats);
            (bool isTargetSuccess, DateTime targetDate) = ToDateTime(targetDateText, formats);

            if (isCurrentSuccess && isTargetSuccess)
            {
                return TimeSpan(currentDate, targetDate, timeUnit);
            }
            return 0;
        }
        /// <summary>
        /// TimeSpan
        /// </summary>
        /// <param name="currentDateText"></param>
        /// <param name="targetDateText"></param>
        /// <param name="timeUnit"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static double TimeSpan(DateTime currentDate, DateTime targetDate, string timeUnit)
        {
            TimeSpan span = (targetDate - currentDate);
            switch (timeUnit.ToLower())
            {
                case DateTimeUnit.Year:
                    return span.TotalDays / 365;
                case DateTimeUnit.Month:
                    return span.TotalDays / 30;
                case DateTimeUnit.Week:
                    return span.TotalDays / 7;
                case DateTimeUnit.Day:
                    return span.TotalDays;
                case DateTimeUnit.Hour:
                    return span.TotalHours;
                case DateTimeUnit.Minute:
                    return span.TotalMinutes;
                case DateTimeUnit.Second:
                    return span.TotalSeconds;
            }
            return 0;
        }

        #endregion


        #region Private Function

        private const string DefaultDateFormat = "yyyy-MM-dd";
        private const string DefaultTimeFormat = "HH:mm:ss";
        private const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Convert To DateTime
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private static (bool isSuccess, DateTime dateTime) ToDateTime(string dateText, string format = "")
        {
            if (!string.IsNullOrEmpty(format))
            {
                if (DateTime.TryParseExact(dateText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime exactDate))
                {
                    return (true, exactDate);
                }
            }

            if (DateTime.TryParse(dateText, out DateTime date))
            {
                return (true, date);
            }

            return (false, new DateTime());
        }
        /// <summary>
        /// Convert To DateTime
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private static (bool isSuccess, DateTime dateTime) ToDateTime(string dateText, IEnumerable<string> formats = null)
        {
            if (formats != null && formats.ToArray().Length != 0)
            {
                if (DateTime.TryParseExact(dateText, formats.ToArray(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime exactDate))
                {
                    return (true, exactDate);
                }
            }

            if (DateTime.TryParse(dateText, out DateTime date))
            {
                return (true, date);
            }

            return (false, new DateTime());
        }

        /// <summary>
        /// Date Time Unit
        /// </summary>
        private static class DateTimeUnit
        {
            public const string Year = "year";
            public const string Month = "month";
            public const string Week = "week";
            public const string Day = "day";
            public const string Hour = "hour";
            public const string Minute = "minute";
            public const string Second = "second";
        }

        #endregion
    }
}
