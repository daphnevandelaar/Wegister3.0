using System;
using Domain.Entities.Abstracts;

namespace Domain.Entities
{
    public class WorkHour : AuditableEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int EmployerId { get; set; }
        public Guid UserId { get; set; }

        public int TotalWorkHoursInMinutes => (int)(EndTime - StartTime).TotalMinutes - RecreationInMinutes;

        public Employer Employer { get; set; }
        public User User { get; set; }
    }
}
