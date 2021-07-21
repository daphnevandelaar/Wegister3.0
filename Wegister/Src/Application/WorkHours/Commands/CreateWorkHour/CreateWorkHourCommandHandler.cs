using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class CreateWorkHourCommandHandler : IRequestHandler<CreateWorkHourCommand>
    {
        private readonly IWegisterDbContext _context;
        private readonly IMediator _mediator;
        private readonly IWorkHourFactory _factory;

        public CreateWorkHourCommandHandler(IWegisterDbContext context, IMediator mediator, IWorkHourFactory factory)
        {
            _context = context;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateWorkHourCommand request, CancellationToken cancellationToken)
        {
            var workHour = _factory.Create(request);

            _context.WorkHours.Add(workHour);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(workHour), cancellationToken);

            return Unit.Value;
        }
    }
}
