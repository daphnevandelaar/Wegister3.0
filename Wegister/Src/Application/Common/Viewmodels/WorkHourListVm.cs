using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Viewmodels
{
    public class WorkHourListVm
    {
        public WorkHourListVm(IList<WorkHourVm> workHours, IList<FilterValueVm> filterValues)
        {
            WorkHours = workHours;
            FilterValues = filterValues;

            TotalAmountOfWorkHoursInMinutes =
                workHours.Sum(w => w.TotalWorkHoursInMinutes += TotalAmountOfWorkHoursInMinutes);
        }

        public IEnumerable<WorkHourVm> WorkHours { get; set; }
        public IList<FilterValueVm> FilterValues { get; }
        public int TotalAmountOfWorkHoursInMinutes { get; }

        public PaginationVm Pagination { get; set; }
    }
}
