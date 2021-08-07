using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries.GetItemsList
{
    public class GetItemsListQueryHandler : IRequestHandler<GetItemsListQuery, ItemListVm>
    {
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IItemFactory _factory;

        public GetItemsListQueryHandler(IWegisterDbContextFactory contextFactory, IItemFactory factory)
        {
            _contextFactory = contextFactory;
            _factory = factory;
        }

        public async Task<ItemListVm> Handle(GetItemsListQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();

            var items = await dbContext.Items
                .Select(i => _factory.CreateLookUpDto(i))
                .ToListAsync(cancellationToken);

            return _factory.Create(items);
        }
    }
}
