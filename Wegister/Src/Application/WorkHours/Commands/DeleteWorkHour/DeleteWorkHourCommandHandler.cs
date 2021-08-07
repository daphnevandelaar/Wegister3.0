using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Builders.Interfaces;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkHours.Commands.DeleteWorkHour
{
    public class DeleteWorkHourCommandHandler : IRequestHandler<DeleteWorkHourCommand>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IMediator _mediator;
        private readonly IWorkHourFactory _factory;
        private readonly IWorkHourBuilder _builder;
        private readonly ICurrentUserService _currentUserService;

        public DeleteWorkHourCommandHandler(IWegisterDbContextFactory contextFactory, IMediator mediator, IWorkHourFactory factory, ICurrentUserService currentUserService, IWorkHourBuilder workHourBuilder)
        {
            _contextFactory = contextFactory;
            _mediator = mediator;
            _factory = factory;
            _currentUserService = currentUserService;
            _builder = workHourBuilder;
        }

        public async Task<Unit> Handle(DeleteWorkHourCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();
            try
            {
                var workhour = _factory.Create(request);

                dbContext.WorkHours.Attach(workhour);
                dbContext.WorkHours.Remove(workhour);
                await dbContext.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(request.Id, cancellationToken);
            }
            catch(Exception ex)
            {

            }

            return Unit.Value;
        }
    }
}
