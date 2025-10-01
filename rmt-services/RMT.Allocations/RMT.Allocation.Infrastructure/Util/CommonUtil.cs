using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Infrastructure.Util
{
    public static class ExtensionMethods
    {
        public static DateTime? GetLocalDate(this DateTime? dt)
        {
            if (dt.HasValue && dt != null)
                return GetLocalDate(((DateTime)dt));
            else
            {
                return null;
            }
        }

        public static DateTime GetLocalDate(this DateTime dt)
        {
            return ((DateTime)dt).ToLocalTime();
        }

        public static DateTime? GetUniversalTime(this DateTime? dt)
        {
            if (dt.HasValue && dt != null)
                return GetUniversalTime(((DateTime)dt));
            else
            {
                return null;
            }
        }

        public static DateTime GetUniversalTime(this DateTime dt)
        {
            return ((DateTime)dt).ToUniversalTime();
        }

        public static string GetDateTimeByFormat(this DateTime? dt, string? format)
        {
            if (dt.HasValue && dt != null)
                return ((DateTime)dt).ToString(format);
            else
            {
                return string.Empty;
            }
        }

        public static DateTime? GetLocalTime(this DateTime? dt)
        {
            if (dt.HasValue && dt != null)
                return ((DateTime)dt).ToLocalTime();
            else
            {
                return null;
            }
        }

        public static DateTime? GetStartDateTime(this DateTime? dt)
        {
            if (dt.HasValue && dt != null)
                return ((DateTime)dt).Date;
            else
            {
                return null;
            }
        }

        public static DateTime? GetEndDateTime(this DateTime? dt)
        {
            if (dt.HasValue && dt != null)
                return ((DateTime)dt).Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            else
            {
                return null;
            }
        }
    }

    public static class CommonUtil
    {
        public static class Constansts
        {
            public static class DateFormatOptions
            {
                public const string DDMMYYYY = "dd-MM-YYYY hh:mm:ss Z";
                public const string YYYYMMDD = "YYYY-MM-dd hh:mm:ss Z";
            }
        }

        public static Boolean isWeekdays(DateTime dt)
        {
            //weekend definition will be configurable
            //return dt.DayOfWeek != DayOfWeek.Sunday && dt.DayOfWeek != DayOfWeek.Saturday;
            return !Constants.NonWorkingDays.Contains(((short)dt.DayOfWeek));
        }

        public static Int64 totalDaysPerDaysWise(DateTime startDate, DateTime endDate, Int64 perDayHours)
        {
            Int64 hours = 0;
            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                if (isWeekdays(dt))
                {
                    hours += perDayHours;
                }
            }
            return hours;
        }


    }
}
