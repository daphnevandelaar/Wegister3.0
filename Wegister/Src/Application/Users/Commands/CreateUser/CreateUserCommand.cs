using System;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; }
        public string Username { get; }
        public string DisplayName { get; }

        public CreateUserCommand(Guid id, string displayName, string username, Guid companyId)
        {
            Id = id;
            DisplayName = displayName;
            Username = username;
            CompanyId = companyId;
        }
    }
}