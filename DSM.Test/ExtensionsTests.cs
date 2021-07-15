using System;
using Xunit;

namespace DSM.Test
{

    public class ExtensionsTests
    {
        [Theory]
        [InlineData("2021-07-08", "2021-07-09")]
        public void ToNextWeekDay_ReturnsNextWeekday(DateTime dateTime, DateTime expected)
        {
            dateTime = dateTime.ToNextWeekDay();

            Assert.Equal(expected, dateTime);
        }

        [Theory]
        [InlineData("2021-07-10")]
        public void IsWeekDay_WithWeekendDate_ReturnsFalse(DateTime dateTime)
        {
            bool result = dateTime.IsWeekDay();

            Assert.False(result);
        }

        [Theory]
        [InlineData("2021-07-08")]
        public void IsWeekDay_WithWeekDayDate_ReturnsTrue(DateTime dateTime)
        {
            bool result = dateTime.IsWeekDay();

            Assert.True(result);
        }

        [Theory]
        [InlineData("2021-07-08", "8:00 AM", "2021-07-08 8:00 AM")]
        public void SetTime_ReturnsDateTimeWithTime(DateTime dateTime, string time, DateTime expected)
        {
            dateTime = dateTime.SetTime(time);

            Assert.Equal(expected, dateTime);

        }

        [Theory]
        [InlineData("2021-07-08", Day.Monday, "2021-07-12")]
        public void ToNextDayOfWeek_ReturnsDateTime(DateTime dateTime, Day dayOfWeek, DateTime expected)
        {
            dateTime = dateTime.ToNextDayOfWeek(dayOfWeek);

            Assert.Equal(expected, dateTime);
        }
    }
}
