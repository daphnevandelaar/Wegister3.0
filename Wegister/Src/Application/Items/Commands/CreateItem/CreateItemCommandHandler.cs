using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IMediator _mediator;
        private readonly IItemFactory _factory;

        public CreateItemCommandHandler(IWegisterDbContextFactory contextFactory, IMediator mediator, IItemFactory factory)
        {
            _contextFactory = contextFactory;
            _mediator = mediator;
            _factory = factory;
        }

        public async Task<Unit> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();

            var item = _factory.Create(request);

            dbContext.Items.Add(item);
            await dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(_factory.Create(item), cancellationToken);

            return Unit.Value;
        }
    }
}
