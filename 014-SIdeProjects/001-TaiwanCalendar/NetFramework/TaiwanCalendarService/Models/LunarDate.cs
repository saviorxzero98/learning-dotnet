namespace TaiwanCalendarService.Models
{
    public class LunarDate
    {
        public int Year { get; private set; }
        public int Month { get; private set; }
        public int Day { get; private set; }
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public bool IsLeapMonth { get; set; }

        public LunarDate(int lunarYear, int lunarMonth, int lunarDay, bool isLeapMonth, 
                         int hour = 0, int minute = 0, int second = 0)
        {
            Year = lunarYear;
            Month = lunarMonth;
            Day = lunarDay;
            Hour = hour;
            Minute = minute;
            Second = second;
            IsLeapMonth = isLeapMonth;
        }
    }
}
