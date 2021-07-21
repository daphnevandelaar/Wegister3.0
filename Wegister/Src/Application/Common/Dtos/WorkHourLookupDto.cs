using System;

namespace Application.Common.Dtos
{
    public class WorkHourLookupDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int TotalWorkHoursInMinutes { get; set; }
        public EmployerMiniDto Employer { get; set; }
        public UserDto User{ get; set; }
    }
}
