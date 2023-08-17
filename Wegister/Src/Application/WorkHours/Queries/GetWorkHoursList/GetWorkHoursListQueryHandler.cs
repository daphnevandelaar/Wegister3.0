using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using Application.Common.Interfaces;
using Application.Common.Viewmodels;
using Application.WorkHours.Queries.GetWorkHoursList;
using Domain.Entities;
using MediatR;

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
            using var dbContext = _contextFactory.CreateDbContext();

            var workhours = dbContext.WorkHours
                .Select(w => new WorkHourLookupDto()
                    {
                        Id = w.Id,
                        StartTime = w.StartTime,
                        EndTime = w.EndTime,
                        RecreationInMinutes = w.RecreationInMinutes,
                        TotalWorkHoursInMinutes = w.TotalWorkHoursInMinutes,
                        Customer = new CustomerMiniDto
                        {
                            Id = w.Customer.Id,
                            Name = w.Customer.Name
                        },
                        Description = w.Description
                    })
                .ToList();

            //var workhours2 = dbContext.WorkHours
            //    .Select())
            //    .ToList();

            return _factory.Create(workhours);
        }
    }
}
