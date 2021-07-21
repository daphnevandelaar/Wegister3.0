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
        private readonly IWegisterDbContext _context;
        private readonly IItemFactory _factory;

        public GetItemsListQueryHandler(IWegisterDbContext context, IItemFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<ItemListVm> Handle(GetItemsListQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items
                .Select(i => _factory.CreateLookUpDto(i))
                .ToListAsync(cancellationToken);

            return _factory.Create(items);
        }
    }
}
