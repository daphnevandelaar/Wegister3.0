using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System;
using Application.Common.Viewmodels;

namespace WegisterUI.Controls
{
    public partial class FilterDropdown
    {
        bool show;

        [Parameter]
        public FilterValueVm FilterOption { get; set; }
        [Parameter]
        public List<string> FilterValues { get; set; }
        [Parameter]
        public EventCallback<FilterValueVm> OnItemSelected { get; set; }
        [Parameter]
        public string Placeholder { get; set; } = "No placeholder specified";

        void Open() => show = !show;

        public void OnValueSelected(ChangeEventArgs e, string value)
        {
            bool isChecked = Convert.ToBoolean(e.Value);

            if (isChecked)
            {
                FilterOption.SelectedValues.Add(value);
            }
            else
            {
                FilterOption.SelectedValues.Remove(value);
            }
            OnItemSelected.InvokeAsync(FilterOption);
        }
    }
}
