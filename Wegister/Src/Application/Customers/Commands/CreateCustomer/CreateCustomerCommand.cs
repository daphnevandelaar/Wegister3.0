using Application.Common.Models;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : AddressBase, IRequest
    {
        public string Name { get; }
        public string Email { get; }
        public string EmailToSendHoursTo { get; }

        public CreateCustomerCommand(string name, string email, string emailToSendHoursTo, string street, string houseNumber, string place, string zipCode)
        :base(street, houseNumber, place, zipCode)
        {
            Name = name;
            Email = email;
            EmailToSendHoursTo = emailToSendHoursTo;
        }
    }
}
