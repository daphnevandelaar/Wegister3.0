using System;

namespace Application.Common.Viewmodels
{
    public class WorkHourVm
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int EmployerId { get; set; }
        public string UserId { get; set; }
        public int TotalWorkHoursInMinutes { get; set; }
    }
}
