using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IWegisterDbContext _context;
        private readonly IMediator _mediator;
        private readonly IItemFactory _factory;

        public CreateItemCommandHandler(IWegisterDbContext context, IMediator mediator, IItemFactory factory)
        {
            _context = context;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = _factory.Create(request);

            _context.Items.Add(item);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(item), cancellationToken);

            return Unit.Value;
        }
    }
}
