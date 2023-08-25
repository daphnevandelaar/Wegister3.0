using Application.Common.Viewmodels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WegisterUI.Pages.WorkHours
{
    public partial class WorkHourOverview
    {
        private WorkHourListVm _workHours;
        private WorkHourListVm _workHoursInit;
        private string _dataTarget = "toggle";
        private List<FilterValueVm> _filters;
        private PaginationVm _pagination;

        protected override async Task OnInitializedAsync()
        {
            _pagination = new()
            {
                Page = 1,
                Take = 200,
                PageSize = 10,
            };
            _filters = new();
            _workHoursInit = await _workHourService.GetWorkHours(_filters, _pagination);
            _workHours = _workHoursInit;
            _filters = _workHours.FilterValues.ToList();
            Pagination(_workHours.WorkHours, _pagination);
        }

        void Navigate()
        {
            _uriHelper.NavigateTo("uren/toevoegen");
        }

        protected async Task OnFilterSelected(FilterValueVm filterValue)
        {
            _workHours = await _workHourService.GetWorkHours(_filters, _pagination);
            _filters = _workHours.FilterValues.ToList();
        }

        protected async Task OnPageSizeSelected(PaginationVm pagination)
        {
            _pagination = pagination;
            _workHours = _workHours = await _workHourService.GetWorkHours(_filters, _pagination);
        }

        protected async Task OtherPageSelected(int selectedPagenumber)
        {
            if(_pagination.Total <= selectedPagenumber * _pagination.PageSize)
            {
                _pagination.Page = selectedPagenumber;
                Pagination(_workHoursInit.WorkHours, _pagination);
            }
            else
            {
                _pagination.Page = selectedPagenumber;
                _workHours = _workHours = await _workHourService.GetWorkHours(_filters, _pagination);
                _workHoursInit = _workHours;
                Pagination(_workHoursInit.WorkHours, _pagination);
            }
        }

        private void Pagination(IEnumerable<WorkHourVm> workHours, PaginationVm pagination)
        {
            if (pagination.PageSize > 0)
                _workHours.WorkHours = workHours.Skip(pagination.PageSize * pagination.Page);
            if (pagination.Skip == 0)
                _workHours.WorkHours = workHours.Take(pagination.PageSize);
            else
                _workHours.WorkHours = workHours
                    .Skip(pagination.Skip)
                    .Take(pagination.PageSize);
        }
    }
}
