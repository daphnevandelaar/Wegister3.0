using System;

namespace WebUI.Dtos
{
    public class WorkHourDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int TotalWorkHoursInMinutes { get; set; }
    }
}
