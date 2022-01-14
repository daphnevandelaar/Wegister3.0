using System.Collections.Generic;
using Application.Common.Viewmodels;
using MediatR;

namespace Application.WorkHours.Queries.GetWorkHourFiltersList
{
    public class GetWorkHourFilterQuery : IRequest<List<FilterValueListVm>>
    {
        public List<FilterValueSelectionVm> SelectedFilters { get; }

        public GetWorkHourFilterQuery(List<FilterValueSelectionVm> selectedFilters)
        {
            SelectedFilters = selectedFilters;
        }
    }
}
