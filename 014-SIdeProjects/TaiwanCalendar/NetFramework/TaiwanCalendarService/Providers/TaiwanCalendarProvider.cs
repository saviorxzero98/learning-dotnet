using System;
using System.Collections.Generic;
using System.Linq;
using TaiwanCalendarService.Models;
using TaiwanCalendarService.Toolkits;

namespace TaiwanCalendarService.Providers
{
    public class TaiwanCalendarProvider : ICalendarProvider
    {       
        /// <summary>
        /// 取得日曆資訊
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<TaiwanCalendarModel> GetCalendars(DateTime date)
        {
            List<TaiwanCalendarModel> holidays = CreateHolidayList(date.Year);

            var calendars = holidays.Where(d => d.Date == date.ToString(TaiwanCalendarModel.DateFormat))
                                    .ToList();

            if (calendars.Any())
            {
                return calendars;
            }
            else
            {
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        return new List<TaiwanCalendarModel>() { new TaiwanCalendarModel(date, true, string.Empty) };
                    default:
                        return new List<TaiwanCalendarModel>() { new TaiwanCalendarModel(date, false, string.Empty) };
                }
            }
        }

        /// <summary>
        /// 取得當年度的假日
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<TaiwanCalendarModel> GetAllCalendar(int year)
        {
            return CreateHolidayList(year);
        }

        /// <summary>
        /// 是否放假
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime date)
        {
            var results = GetCalendars(date);

            if (results != null)
            {
                bool isHoliday = results.Any(c => c.IsHoliday);
                return isHoliday;
            }
            else
            {
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        return true;
                    default:
                        return false;
                }
            }
        }


        #region 建立假日

        /// <summary>
        /// 建立假日清單
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        protected List<TaiwanCalendarModel> CreateHolidayList(int year)
        {
            List<TaiwanCalendarModel> calendars = new List<TaiwanCalendarModel>();

            // 有固定補假規則的節日
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 1, 1), true, "元旦"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 2, 28), true, "二二八和平紀念日"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 4, 4), true, "兒童節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 5, 1), true, "勞動節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 10, 10), true, "雙十國慶日"));

            if (year > 1901 && year < 2100)
            {
                // 清明節計算只計算1901年~2100年區間
                calendars.AddRange(CreateQingmingFestival(year, "清明節"));

                // 因為.NET 農曆計算 (ChineseLunisolarCalendar) 只處理西元1901年~2100年區間
                calendars.Add(CreateHoliday(CalendarUtility.FromLunarDate(year, 5, 5), true, "端午節"));
                calendars.Add(CreateHoliday(CalendarUtility.FromLunarDate(year, 8, 15), true, "中秋節"));
            }

            // 處理補假
            calendars = FixHoliday(calendars);

            // 無補假規則的節日
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 2, 14), false, "情人節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 3, 8), false, "婦女節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 3, 14), false, "白色情人節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 3, 29), false, "青年節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 8, 8), false, "父親節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 9, 28), false, "教師節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 10, 25), false, "光復節"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 11, 12), false, "國父誕辰紀念日"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 12, 25), false, "行憲紀念日"));
            calendars.Add(CreateHoliday(CalendarUtility.Create(year, 12, 25), false, "聖誕節"));
            calendars.Add(CreateHoliday(CalendarUtility.FindFirstDate(year, 5, 2, DayOfWeek.Sunday), false, "母親節"));

            if (year > 1901 && year < 2100)
            {
                // 因為.NET 農曆計算 (ChineseLunisolarCalendar) 只處理西元1901年~2100年區間
                calendars.Add(CreateHoliday(CalendarUtility.FromLunarDate(year, 1, 15), false, "元宵節"));
                calendars.Add(CreateHoliday(CalendarUtility.FromLunarDate(year, 7, 7), false, "七夕"));
                calendars.Add(CreateHoliday(CalendarUtility.FromLunarDate(year, 7, 15), false, "中元節"));
                calendars.Add(CreateHoliday(CalendarUtility.FromLunarDate(year, 9, 9), false, "重陽節"));
            }

            // 特殊補假規則的節日
            if (year > 1901 && year < 2100)
            {
                // 因為.NET 農曆計算 (ChineseLunisolarCalendar) 只處理西元1901年~2100年區間
                calendars.AddRange(CreateChineseNewYear(CalendarUtility.FromLunarDate(year, 1, 1)));
            }

            // 排序時間
            calendars = calendars.OrderBy(c => c.Date).ToList();

            return calendars;
        }

        /// <summary>
        /// 建立假日
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="isHoliday"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        protected TaiwanCalendarModel CreateHoliday(DateTime dateTime, bool isHoliday, string name, string desc = "")
        {
            return new TaiwanCalendarModel(dateTime, isHoliday, name, desc);
        }

        /// <summary>
        /// 建立春節，使用農曆新年特殊算法 (除夕~初三逢週六、週日，初四~初五補假)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        protected List<TaiwanCalendarModel> CreateChineseNewYear(DateTime dateTime, string desc = "新年連假")
        {
            List<TaiwanCalendarModel> calendars = new List<TaiwanCalendarModel>()
            {
                new TaiwanCalendarModel(dateTime.AddDays(-1), true, "除夕", desc),
                new TaiwanCalendarModel(dateTime, true, "大年初一", desc),
                new TaiwanCalendarModel(dateTime.AddDays(1), true, "大年初二", desc),
                new TaiwanCalendarModel(dateTime.AddDays(2), true, "大年初三", desc),
            };

            int fixDays = 0;
            foreach (var calendar in calendars)
            {
                switch (calendar.DateTime.DayOfWeek)
                {
                    case DayOfWeek.Saturday:
                    case DayOfWeek.Sunday:
                        fixDays++;
                        break;
                }
            }

            switch (fixDays)
            {
                case 1:
                    calendars.Add(new TaiwanCalendarModel(dateTime.AddDays(3), true, "大年初四", desc));
                    break;
                case 2:
                    calendars.Add(new TaiwanCalendarModel(dateTime.AddDays(3), true, "大年初四", desc));
                    calendars.Add(new TaiwanCalendarModel(dateTime.AddDays(4), true, "大年初五", desc));
                    break;
            }
            return calendars;
        }

        /// <summary>
        /// 建立清明節
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        protected List<TaiwanCalendarModel> CreateQingmingFestival(int year, string name = "清明節", string desc = "")
        {
            List<TaiwanCalendarModel> calendars = new List<TaiwanCalendarModel>();

            if (TryGetQingmingFestival(year, out DateTime festival))
            {
                calendars.Add(new TaiwanCalendarModel(festival, true, name, desc));
            }
            return calendars;
        }

        /// <summary>
        /// 計算清明節時間
        /// </summary>
        /// <param name="year"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool TryGetQingmingFestival(int year, out DateTime date)
        {
            int leapYear = year % 4;

            if (year >= 1901 && year <= 1911)
            {   // 閏年和下一年是 4/5，其他年份是 4/6
                date = (leapYear < 2) ? new DateTime(year, 4, 5) : new DateTime(year, 4, 6);
            }
            else if (year >= 1912 && year <= 1943)
            {   // 閏年和下兩年是 4/5，其他年份是 4/6
                date = (leapYear < 3) ? new DateTime(year, 4, 5) : new DateTime(year, 4, 6);
            }
            else if (year >= 1944 && year <= 1975)
            {   // 皆為 4/5
                date = new DateTime(year, 4, 5);
            }
            else if (year >= 1976 && year <= 2007)
            {   // 閏年是 4/4，其他年份是 4/5
                date = (leapYear < 1) ? new DateTime(year, 4, 4) : new DateTime(year, 4, 5);
            }
            else if (year >= 2008 && year <= 2039)
            {   // 閏年和下一年是 4/4，其他年份是 4/5
                date = (leapYear < 2) ? new DateTime(year, 4, 4) : new DateTime(year, 4, 5);
            }
            else if (year >= 2040 && year <= 2071)
            {   // 閏年和下兩年是 4/4，其他年份是 4/5
                date = (leapYear < 3) ? new DateTime(year, 4, 4) : new DateTime(year, 4, 5);
            }
            else if (year >= 2072 && year <= 2099)
            {   // 皆為 4/4
                date = new DateTime(year, 4, 4);
            }
            else if (year == 2100)
            {   // 為 4/5
                date = new DateTime(year, 4, 5);
            }
            else
            {
                date = default(DateTime);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 補假處理，逢週六、往前補假；逢週日、往後補假
        /// </summary>
        protected List<TaiwanCalendarModel> FixHoliday(List<TaiwanCalendarModel> calendars)
        {
            List<TaiwanCalendarModel> holidays = calendars.Where(c => c.IsHoliday).ToList();

            foreach (var holiday in holidays)
            {
                if (holiday.DateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    DateTime fixDate = FindFixDate(calendars, holiday.DateTime, -1);
                    calendars.Add(new TaiwanCalendarModel(fixDate, true, $"{holiday.Name} (補假)", holiday.Description));
                }

                if (holiday.DateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    DateTime fixDate = FindFixDate(calendars, holiday.DateTime, 1);
                    calendars.Add(new TaiwanCalendarModel(fixDate, true, $"{holiday.Name} (補假)", holiday.Description));
                }
            }

            return calendars;
        }

        /// <summary>
        /// 尋找補假日
        /// </summary>
        /// <param name="calendars"></param>
        /// <param name="baseDate"></param>
        /// <param name="dayIterator"></param>
        /// <returns></returns>
        protected DateTime FindFixDate(List<TaiwanCalendarModel> calendars, DateTime baseDate, int dayIterator)
        {
            DateTime nextDate = baseDate.AddDays(dayIterator);

            bool isNotFound = calendars.Any(d => d.IsHoliday &&
                                                 d.Date == nextDate.ToString(TaiwanCalendarModel.DateFormat));

            if (isNotFound)
            {
                return FindFixDate(calendars, nextDate, dayIterator);
            }
            return nextDate;
        }

        #endregion
    }
}
