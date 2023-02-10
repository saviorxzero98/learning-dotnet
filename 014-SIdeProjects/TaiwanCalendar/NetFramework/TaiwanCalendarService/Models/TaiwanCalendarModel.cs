using System;
using System.Globalization;
using System.Threading;

namespace TaiwanCalendarService.Models
{
    public class TaiwanCalendarModel
    {
        public const string DateFormat = "yyyy-MM-dd";

        public DateTime DateTime { get; set; }

        public string Name { get; set; }

        public bool IsHoliday { get; set; }

        public string Description { get; set; }

        public string Date
        {
            get
            {
                return DateTime.ToString(DateFormat);
            }
        }

        public TaiwanCalendarModel(DateTime dateTime, bool isHoliday, string name, string desc = "")
        {
            DateTime = dateTime;
            IsHoliday = isHoliday;
            Name = name;
            Description = desc;
        }
    }
}
