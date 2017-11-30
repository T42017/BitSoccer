using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BitSoccerWeb.Temp
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return (long) dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
