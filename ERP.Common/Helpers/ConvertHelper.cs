using System;
using System.Globalization;

namespace ERP.Common.Helpers
{
    public static class ConvertHelper
    {
        //// <summary>
        /// To the integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The integer value.</returns>
        public static int ToInteger(object value)
        {
            if (value == null)
            {
                return 0;
            }

            var val = value.ToString();
            int result = 0;
            int.TryParse(val, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            return result;
        }

        /// <summary>
        /// To the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The decimal value</returns>
        public static decimal ToDecimal(object value)
        {
            var val = value.ToString();
            decimal result = 0;
            decimal.TryParse(val, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result);
            return result;
        }

        /// <summary>
        /// To the date.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The date time value.</returns>
        public static DateTime? ToDate(object value)
        {
            if (value == null)
            {
                return null;
            }

            var val = value.ToString();
            DateTime result;
            if (!DateTime.TryParse(val, out result))
            {
                return null;
            }

            return result;
        }

        /// <summary>
        /// To the time span.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The time span.</returns>
        public static TimeSpan ToTimeSpan(string value)
        {
            var timefield = value.Replace(" : ", ":");
            var fullDateStr = (new DateTime(2000, 1, 1)).ToString("MM/dd/yyyy");
            var timeSplit = timefield.Split(':');

            var minuteMeridian = (timeSplit[1]).Split(' ');
            fullDateStr = $"{fullDateStr} {timeSplit[0]}:{minuteMeridian[0]} {minuteMeridian[1]}";

            var fullDate = DateTime.Parse(fullDateStr);
            var timeSpanVal = TimeSpan.Parse(fullDate.ToString("HH:mm:ss"));

            return timeSpanVal;
        }
    }
}