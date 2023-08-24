using Application.Common.Viewmodels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegisterUI.Controls
{
    public partial class SearchBar
    {
        bool show;
        List<FilterValueVm> dropdownList = new();
        string SelectedFilterValue = "";

        [Parameter]
        public string Placeholder { get; set; } = "No placeholder specified";
        [Parameter]
        public List<FilterValueVm> DropdownlistIn { get; set; }
        [Parameter]
        public EventCallback<FilterValueVm> OnItemSelected { get; set; }

        protected override async Task OnInitializedAsync() => dropdownList = DropdownlistIn;

        void Open() => show = true;
        void Close() => show = false;

        //void FilterOnValue(ChangeEventArgs args) => dropdownList = DropdownlistIn.Where(v => v.Value.Contains(args.Value.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();
        //public void OnValueSelected(FilterValueVm value)
        //{
        //    SelectedFilterValue = value.Value;
        //    OnItemSelected.InvokeAsync(value);
        //    Close();
        //}
    }
}
