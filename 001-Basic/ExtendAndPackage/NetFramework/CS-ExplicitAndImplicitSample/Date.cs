using System;

namespace ExplicitAndImplicitSample
{
    public class Date
    {
        protected DateTime _dateTime;

        public Date(DateTime datetime)
        {
            _dateTime = datetime;
        }

        public string ToString()
        {
            return _dateTime.ToString("yyyy-MM-dd");
        }

        public static Date NextWeek {
            get
            {
                Date date = new Date(DateTime.Today.AddDays(7));
                return date;
            }
        }

        public static explicit operator Date(DateTime dateTime)
        {
            return new Date(dateTime);
        }

        public static implicit operator DateTime(Date date)
        {
            return date._dateTime;
        }
    }
}
