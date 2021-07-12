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
                dateTime = dateTime.AddDays(1);
            } 
            while (!dateTime.IsWeekDay());

            return dateTime;
        }

        /// <summary>
        /// Checks if the day of the week is not
        /// Saturday or Sunday
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsWeekDay(this DateTime dateTime)
        {
            return !(dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday);
        }

        public static DateTime ToNextDayOfWeek(this DateTime dateTime, Day day)
        {
            DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day.ToString());

            int daysToAdd = ((int)dayOfWeek - (int)dateTime.DayOfWeek + 7) % 7;

            return dateTime.AddDays(daysToAdd);
        }

        public static DateTime SetTime(this DateTime dateTime, string time)
        {
            DateTime temp = Convert.ToDateTime(time);

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, temp.Hour, temp.Minute, temp.Second);
        }


    }
}
