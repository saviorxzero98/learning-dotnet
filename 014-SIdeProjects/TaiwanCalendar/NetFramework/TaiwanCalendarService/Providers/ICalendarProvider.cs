using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiwanCalendarService.Providers
{
    public interface ICalendarProvider
    {
        bool IsHoliday(DateTime date);
    }
}
