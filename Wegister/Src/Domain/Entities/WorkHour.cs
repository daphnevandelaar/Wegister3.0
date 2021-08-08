using System;
using Domain.Entities.Abstracts;

namespace Domain.Entities
{
    public class WorkHour : AuditableEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RecreationInMinutes { get; set; }
        public int CustomerId { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }

        public int TotalWorkHoursInMinutes => (int)(EndTime - StartTime).TotalMinutes - RecreationInMinutes;

        public Customer Customer { get; set; }
        public User User { get; set; }
    }
}
