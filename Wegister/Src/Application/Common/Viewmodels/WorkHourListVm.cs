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

        public IList<WorkHourVm> WorkHours { get; }
        public IList<FilterValueVm> FilterValues { get; }
        public int TotalAmountOfWorkHoursInMinutes { get; }
    }
}
