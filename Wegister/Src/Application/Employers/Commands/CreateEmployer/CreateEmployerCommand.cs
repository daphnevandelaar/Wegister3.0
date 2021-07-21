using Application.Common.Models;
using MediatR;

namespace Application.Employers.Commands.CreateEmployer
{
    public class CreateEmployerCommand : AddressBase, IRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailToSendHoursTo { get; set; }
    }
}
