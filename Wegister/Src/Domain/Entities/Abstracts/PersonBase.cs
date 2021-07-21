namespace Domain.Entities.Abstracts
{
    public abstract class PersonBase : AuditableEntity
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Place { get; set; }
        public string ZipCode { get; set; }
    }
}
