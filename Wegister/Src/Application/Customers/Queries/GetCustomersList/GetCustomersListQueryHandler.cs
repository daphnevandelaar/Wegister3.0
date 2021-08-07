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
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly ICustomerFactory _factory;

        public GetCustomersListQueryHandler(IWegisterDbContextFactory contextFactory, ICustomerFactory factory)
        {
            _contextFactory = contextFactory;
            _factory = factory;
        }

        public async Task<CustomerListVm> Handle(GetCustomersListQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();

            var customers = await dbContext.Customers
                .Select(c => _factory.CreateLookUpDto(c))
                .ToListAsync(cancellationToken);

            return _factory.Create(customers);
        }
    }
}
