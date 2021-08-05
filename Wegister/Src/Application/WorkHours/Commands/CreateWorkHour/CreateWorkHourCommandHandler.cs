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
        private readonly IWegisterDbContext _context;
        private readonly IMediator _mediator;
        private readonly IWorkHourFactory _factory;
        private readonly IWorkHourBuilder _builder;
        private readonly ICurrentUserService _currentUserService;

        public CreateWorkHourCommandHandler(IWegisterDbContext context, IMediator mediator, IWorkHourFactory factory, ICurrentUserService currentUserService, IWorkHourBuilder workHourBuilder)
        {
            _context = context;
            _mediator = mediator;
            _factory = factory;
            _currentUserService = currentUserService;
            _builder = workHourBuilder;
        }

        public async Task<Unit> Handle(CreateWorkHourCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var workHour = _factory.Create(request);
                var currentUser = await _context.Users.SingleAsync(u => u.Id == new Guid(_currentUserService.UserId), cancellationToken);
                workHour = _builder.Build(workHour, currentUser);

                _context.WorkHours.Add(workHour);
                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(_factory.Create(workHour), cancellationToken);
            }
            catch(Exception ex)
            {

            }

            return Unit.Value;
        }
    }
}
