using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Builders.Interfaces;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.WorkHours.Commands.CreateWorkHour;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IWegisterDbContextFactory _contextFactory;

        public CreateUserCommandHandler(IWegisterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();
            try
            {
                dbContext.Users.Add(new User
                {
                    Id = request.Id,
                    CompanyId = request.CompanyId.ToString(),
                    DisplayName = request.DisplayName
                });
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // ignored
            }

            return Unit.Value;
        }
    }
}
