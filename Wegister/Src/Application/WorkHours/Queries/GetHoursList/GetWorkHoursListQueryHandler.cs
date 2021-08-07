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
        private readonly IWegisterDbContextFactory _contextFactory;
        private readonly IWorkHourFactory _factory;

        public GetWorkHoursListQueryHandler(IWegisterDbContextFactory contextFactory, IWorkHourFactory factory)
        {
            _contextFactory = contextFactory;
            _factory = factory;
        }

        public async Task<WorkHourListVm> Handle(GetWorkHoursListQuery request, CancellationToken cancellationToken)
        {
            var dbContext = _contextFactory.CreateDbContext();

            var workhours = await dbContext.WorkHours
                .Select(w => _factory.CreateLookUpDto(w))
                .ToListAsync(cancellationToken);

            return _factory.Create(workhours);
        }
    }
}
