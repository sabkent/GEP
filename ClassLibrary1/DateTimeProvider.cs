using System;

namespace ClassLibrary1
{
    /// <summary>
    /// this class enables you to mock time, code must not use DateTime.Now but DateTimeProvider.Now
    /// </summary>
    static class DateTimeProvider
    {
        public static Func<DateTime> DateTimeFunction = () => DateTime.Now;

        public static DateTime Now
        {
            get { return DateTimeFunction(); }
        }
    }
}
