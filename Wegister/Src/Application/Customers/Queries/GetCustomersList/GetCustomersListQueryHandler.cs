using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQueryHandler : IRequestHandler<GetCustomersListQuery, CustomerListVm>
    {
        private readonly IWegisterDbContext _context;
        private readonly ICustomerFactory _factory;

        public GetCustomersListQueryHandler(IWegisterDbContext context, ICustomerFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<CustomerListVm> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers
                .Select(c => _factory.CreateLookUpDto(c))
                .ToListAsync(cancellationToken);

            return _factory.Create(customers);
        }
    }
}
