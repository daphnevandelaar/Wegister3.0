using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Queries.GetWorkHoursList;
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

            var workhours = dbContext.WorkHours
                .Include(w => w.Employer)
                .Select(w => _factory.CreateLookUpDto(w))
                .ToList();

            return _factory.Create(workhours);
        }
    }
}
