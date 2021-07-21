using System.Collections.Generic;
using System.Linq;
using Application.Common.Dtos;

namespace Application.Common.Viewmodels
{
    public class WorkHourListVm
    {
        public WorkHourListVm(IList<WorkHourLookupDto> workHours)
        {
            WorkHours = workHours;
            TotalAmountOfWorkHoursInMinutes =
                workHours.Sum(w => w.TotalWorkHoursInMinutes += TotalAmountOfWorkHoursInMinutes);
        }

        public IList<WorkHourLookupDto> WorkHours { get; }
        public int TotalAmountOfWorkHoursInMinutes { get; }
    }
}
