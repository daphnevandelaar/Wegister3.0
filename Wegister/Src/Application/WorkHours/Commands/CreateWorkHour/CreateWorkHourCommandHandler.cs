using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Builders.Interfaces;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class CreateWorkHourCommandHandler : IRequestHandler<CreateWorkHourCommand>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IMediator _mediator;
        private readonly IWorkHourFactory _factory;
        private readonly IWorkHourBuilder _builder;
        private readonly ICurrentUserService _currentUserService;

        public CreateWorkHourCommandHandler(IWegisterDbContextFactory contextFactory, IMediator mediator, IWorkHourFactory factory, ICurrentUserService currentUserService, IWorkHourBuilder workHourBuilder)
        {
            _contextFactory = contextFactory;
            _mediator = mediator;
            _factory = factory;
            _currentUserService = currentUserService;
            _builder = workHourBuilder;
        }

        public async Task<Unit> Handle(CreateWorkHourCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();
            try
            {
                var workHour = _factory.Create(request);
                var currentUser = await dbContext.Users.SingleAsync(u => u.Id == new Guid(_currentUserService.CreateSession().UserId), cancellationToken);
                workHour = _builder.Build(workHour, currentUser);

                dbContext.WorkHours.Add(workHour);
                await dbContext.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(_factory.Create(workHour), cancellationToken);
            }
            catch(Exception ex)
            {

            }

            return Unit.Value;
        }
    }
}
