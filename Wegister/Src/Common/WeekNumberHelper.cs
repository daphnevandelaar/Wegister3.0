using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Common
{
    public static class WeekNumberHelper
    {
        public static int GetWeeknumber(DateTime date)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNo = currentCulture.Calendar.GetWeekOfYear(
                date,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

            return weekNo;
        }

        public static int GetWeeknumberFromString(string date)
        {
            var matchDay = new Regex(@"^\d{2}");
            var matchMonth = new Regex(@"(?<=\/)\d{2}(?=\/)");
            var matchYear = new Regex(@"(?<=\/)\d{3}?[^\/]");

            var day = Convert.ToInt32(matchDay.Match(date).Value);
            var month = Convert.ToInt32(matchMonth.Match(date).Value);
            var year = Convert.ToInt32(matchYear.Match(date).Value);

            var dateToGetWeekFrom = new DateTime(year, month, day);

            return GetWeeknumber(dateToGetWeekFrom);
        }

        public static DateTime GetEndDateFromWeekYearString(string weekAndYear)
        {
            return GetEndDateFromWeekNumberAndYear(GetWeekFromString(weekAndYear), GetYearFromString(weekAndYear));
        }

        public static int GetWeekFromString(string weekAndYear)
        {
            var matchWeekNumber = new Regex(@"(?<=Week )(.*)(?=/)");
            return Convert.ToInt32(matchWeekNumber.Match(weekAndYear).Value);
        }

        public static int GetYearFromString(string weekAndYear)
        {
            var matchYear = new Regex(@"(?<=\/).*");
            return Convert.ToInt32(matchYear.Match(weekAndYear).Value);
        }

        public static DateTime GetEndDateFromWeekNumberAndYear(int weeknumber, int year)
        {
            var dateTime = new DateTime(year, 1, 1);
            var currentCulture = CultureInfo.CurrentCulture;
            var result = currentCulture.Calendar.AddWeeks(dateTime, weeknumber);

            return result;
        }
    }
}
