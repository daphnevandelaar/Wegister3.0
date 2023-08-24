using System;
using System.Collections.Generic;

namespace Application.WorkHours.Queries.GetWorkHoursList
{
    public class GetWorkHoursListQueryFilters
    {
        public List<WeekNumberDate> WeekNumberFilters { get; set; }
        public List<string> YearFilters { get; set; }
        public List<string> CustomerFilters { get; set; }
    }

    public class WeekNumberDate
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Weeknumber { get; set; }
        public int Year { get; set; }
    }
}
