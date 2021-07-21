using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly IWegisterDbContext _context;
        private readonly IMediator _mediator;
        private readonly ICustomerFactory _factory;

        public CreateCustomerCommandHandler(IWegisterDbContext context, IMediator mediator, ICustomerFactory factory)
        {
            _context = context;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = _factory.Create(request);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(customer), cancellationToken);

            return Unit.Value;
        }
    }
}
