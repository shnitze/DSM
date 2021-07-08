using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSM
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a DateTime with the day set to the next weekday
        /// </summary>
        /// <returns></returns>
        public static DateTime ToNextWeekDay(this DateTime dateTime)
        {

            do
            {
                dateTime.AddDays(1);
            } 
            while (!dateTime.IsWeekday());

            return dateTime;
        }

        public static bool IsWeekday(this DateTime dateTime)
        {
            return !(dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }
    }
}
