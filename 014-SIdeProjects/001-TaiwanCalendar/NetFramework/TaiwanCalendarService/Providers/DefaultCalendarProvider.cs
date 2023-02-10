using System;

namespace TaiwanCalendarService.Providers
{
    public class DefaultCalendarProvider : ICalendarProvider
    {
        /// <summary>
        /// 取得是否放假
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public bool IsHoliday(DateTime date)
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
}
