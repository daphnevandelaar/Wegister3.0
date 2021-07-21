using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Employers.Commands.CreateEmployer
{
    public class CreateEmployerCommandHandler : IRequestHandler<CreateEmployerCommand>
    {
        private readonly IWegisterDbContext _context;
        private readonly IMediator _mediator;
        private readonly IEmployerFactory _factory;

        public CreateEmployerCommandHandler(IWegisterDbContext context, IMediator mediator, IEmployerFactory factory)
        {
            _context = context;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {
            var employer = _factory.Create(request);

            _context.Employers.Add(employer);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(employer), cancellationToken);

            return Unit.Value;
        }
    }
}
