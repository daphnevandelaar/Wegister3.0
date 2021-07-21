using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Employers.Queries.GetEmployersList
{
    public class GetEmployersListQueryHandler : IRequestHandler<GetEmployersListQuery, EmployerListVm>
    {
        private readonly IWegisterDbContext _context;
        private readonly IEmployerFactory _factory;

        public GetEmployersListQueryHandler(IWegisterDbContext context, IEmployerFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<EmployerListVm> Handle(GetEmployersListQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Employers
                .Select(e => _factory.CreateLookUpDto(e))
                .ToListAsync(cancellationToken);

            return _factory.Create(customers);
        }
    }
}
