using System.Collections.Generic;
using Application.Common.Viewmodels;
using MediatR;

namespace Application.Customers.Queries.SearchCustomerList
{
    public class SearchCustomerListQuery : IRequest<List<SearchVm>>
    {
        public string SearchText { get; }

        public SearchCustomerListQuery(string searchText)
        {
            SearchText = searchText;
        }
    }
}
