using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.WorkHours.Queries.GetHoursList
{
    public class GetWorkHoursListQueryHandler : IRequestHandler<GetWorkHoursListQuery, WorkHourListVm>
    {
        private readonly IWegisterDbContext _context;
        private readonly IWorkHourFactory _factory;

        public GetWorkHoursListQueryHandler(IWegisterDbContext context, IWorkHourFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<WorkHourListVm> Handle(GetWorkHoursListQuery request, CancellationToken cancellationToken)
        {
            var workhours = await _context.WorkHours
                .Select(w => _factory.CreateLookUpDto(w))
                .ToListAsync(cancellationToken);

            return _factory.Create(workhours);
        }
    }
}
