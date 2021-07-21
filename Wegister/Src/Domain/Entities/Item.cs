using Domain.Entities.Abstracts;

namespace Domain.Entities
{
    public class Item : AuditableEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
    }
}
