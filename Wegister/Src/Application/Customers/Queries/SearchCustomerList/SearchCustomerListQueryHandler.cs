using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.SearchCustomerList
{
    public class SearchCustomerListQueryHandler : IRequestHandler<SearchCustomerListQuery, List<SearchVm>>
    {
        private readonly IWegisterDbContext _context;
        private readonly ICustomerFactory _factory;

        public SearchCustomerListQueryHandler(IWegisterDbContext context, ICustomerFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<List<SearchVm>> Handle(SearchCustomerListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers
                .Where(c => EF.Functions.Like(c.Name, $"%{request.SearchText}%"))
                .Select(c => _factory.CreateDto(c))
                .ToListAsync(cancellationToken);

            return _factory.CreateCommon(customers);
        }
    }
}
