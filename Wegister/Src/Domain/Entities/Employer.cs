using Domain.Entities.Abstracts;

namespace Domain.Entities
{
    public class Employer : PersonBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailToSendHoursTo { get; set; }
    }
}
