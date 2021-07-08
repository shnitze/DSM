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
    }
}
