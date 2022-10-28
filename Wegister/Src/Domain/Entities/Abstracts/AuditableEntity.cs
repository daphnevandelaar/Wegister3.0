using System;

namespace Domain.Entities.Abstracts
{
    public abstract class AuditableEntity : EntityBase
    {
        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
