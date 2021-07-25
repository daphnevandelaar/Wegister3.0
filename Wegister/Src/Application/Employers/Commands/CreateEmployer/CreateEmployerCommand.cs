using Application.Common.Models;
using MediatR;

namespace Application.Employers.Commands.CreateEmployer
{
    public class CreateEmployerCommand : AddressBase, IRequest
    {
        public string Name { get; }
        public string Email { get; }
        public string EmailToSendHoursTo { get; }

        public CreateEmployerCommand(string name, string email, string emailToSendHoursTo, string street, string houseNumber, string place, string zipCode)
            : base(street, houseNumber, place, zipCode)
        {
            Name = name;
            Email = email;
            EmailToSendHoursTo = emailToSendHoursTo;
        }
    }
}
