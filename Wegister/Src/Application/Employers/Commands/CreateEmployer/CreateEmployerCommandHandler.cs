using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Employers.Commands.CreateEmployer
{
    public class CreateEmployerCommandHandler : IRequestHandler<CreateEmployerCommand>
    {
        private readonly IWegisterDbContextFactory _contextFactory;

        private readonly IWegisterDbContext _context;
        private readonly IMediator _mediator;
        private readonly IEmployerFactory _factory;

        public CreateEmployerCommandHandler(IWegisterDbContextFactory contextFactory, IMediator mediator, IEmployerFactory factory)
        {
            _contextFactory = contextFactory;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();

            var employer = _factory.Create(request);

            dbContext.Employers.Add(employer);

            await dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(employer), cancellationToken);

            return Unit.Value;
        }
    }
}
