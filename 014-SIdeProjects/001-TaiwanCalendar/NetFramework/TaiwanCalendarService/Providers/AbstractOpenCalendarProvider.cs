using System;
using System.Collections.Generic;
using System.Linq;
using TaiwanCalendarService.Models;

namespace TaiwanCalendarService.Providers
{
    public abstract class AbstractOpenCalendarProvider : ICalendarProvider
    {
        private const string Yes = "是";
        private const string No = "否";
        private const string DateFormat = "yyyy/M/d";

        protected List<OpenCalendarModel> _calendar = new List<OpenCalendarModel>();

        // 前一次向開放平台取得資料的時間 (年份)
        protected DateTime _lastUpdateDateTime;
        public int LastUpdateYear { get { return _lastUpdateDateTime.Year; } }

        /// <summary>
        /// 讀取資料
        /// </summary>
        /// <returns></returns>
        public abstract List<OpenCalendarModel> LoadCalendar();

        /// <summary>
        /// 取得放假日或彈性上班日資料
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual OpenCalendarModel GetOpenCalendar(DateTime date)
        {
            string dateText = date.ToString(DateFormat);

            return GetOpenCalendar(dateText);
        }
        /// <summary>
        /// 取得放假日或彈性上班日資料
        /// </summary>
        /// <param name="dateText">Format 'yyyy/M/d'</param>
        /// <returns></returns>
        public virtual OpenCalendarModel GetOpenCalendar(string dateText)
        {
            return _calendar.Where(d => d.Date == dateText)
                            .FirstOrDefault();
        }

        /// <summary>
        /// 是否為放假日
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public virtual bool IsHoliday(DateTime date)
        {
            var result = GetOpenCalendar(date);

            // 無資料，則為上班日
            if (result == null)
            {
                return false;
            }

            switch (result.IsHoliday)
            {
                case Yes:
                    return true;
                case No:
                    return false;
                default:
                    return false;
            }
        }
    }
}
