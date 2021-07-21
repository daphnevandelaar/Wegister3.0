using System;
using Domain.Entities.Abstracts;

namespace Domain.Entities
{
    public class WorkHour : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int TotalWorkHoursInMinutes { get; set; }
        public Employer Employer { get; set; }
        public User User { get; set; }
    }
}
