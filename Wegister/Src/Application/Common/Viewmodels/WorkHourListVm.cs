using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Viewmodels
{
    public class WorkHourListVm
    {
        public WorkHourListVm(IList<WorkHourVm> workHours)
        {
            WorkHours = workHours;
            TotalAmountOfWorkHoursInMinutes =
                workHours.Sum(w => w.TotalWorkHoursInMinutes += TotalAmountOfWorkHoursInMinutes);
        }

        public IList<WorkHourVm> WorkHours { get; }
        public int TotalAmountOfWorkHoursInMinutes { get; }
    }
}
