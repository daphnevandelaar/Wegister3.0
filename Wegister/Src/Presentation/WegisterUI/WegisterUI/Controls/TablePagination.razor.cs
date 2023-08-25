using System.Threading.Tasks;
using Application.Common.Viewmodels;

namespace WegisterUI.Controls
{
    public partial class TablePagination
    {
        public PaginationVm Pagination { get; set; }
        private int _amountOfPages { get; set; }
        private string _pagenumber;

        protected override async Task OnInitializedAsync()
        {
            Pagination = new PaginationVm()
            {
                Page = 1,
                PageSize = 10,
                Total = 100
            };
            _amountOfPages = Pagination.Total / Pagination.PageSize;
        }
        protected void OnClickPage(int pageNumber)
        {
            if(pageNumber == -1)
            {
                if(Pagination.Page != 1)
                    Pagination.Page--;
            }
            else if(pageNumber == -11)
            {
                if(Pagination.Page != 1)
                    Pagination.Page = 1;
            }
            else if (pageNumber == -2)
            {
                if(Pagination.Page != _amountOfPages)
                    Pagination.Page++;
            }
            else if (pageNumber == -22)
            {
                Pagination.Page = _amountOfPages;
            }
            else
            {
                Pagination.Page = pageNumber;
            }

            _pagenumber = pageNumber.ToString();
        }
    }
}
