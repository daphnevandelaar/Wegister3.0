using Application.Common.Viewmodels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegisterUI.Pages.WorkHours
{
    public partial class WorkHourOverview
    {
        private WorkHourListVm _workHours;
        private string _dataTarget = "toggle";
        private List<FilterValueVm> _filters;

        protected override async Task OnInitializedAsync()
        {
            _workHours = await _workHourService.GetWorkHours();
            _filters = _workHours.FilterValues.ToList();
        }

        void Navigate()
        {
            _uriHelper.NavigateTo("uren/toevoegen");
        }

        protected async Task OnFilterSelected(FilterValueVm filterValue)
        {
            var selectedFilters = _filters.Where(f => f.SelectedValues.Any());

        }
    }
}
