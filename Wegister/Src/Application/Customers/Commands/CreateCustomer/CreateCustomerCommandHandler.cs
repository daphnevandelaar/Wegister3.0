using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IMediator _mediator;
        private readonly ICustomerFactory _factory;

        public CreateCustomerCommandHandler(IWegisterDbContextFactory contextFactory, IMediator mediator, ICustomerFactory factory)
        {
            _contextFactory = contextFactory;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();

            var customer = _factory.Create(request);

            dbContext.Customers.Add(customer);
            await dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(customer), cancellationToken);

            return Unit.Value;
        }
    }
}
