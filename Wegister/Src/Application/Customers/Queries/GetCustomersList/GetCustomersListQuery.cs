using Application.Common.Viewmodels;
using MediatR;

namespace Application.Customers.Queries.GetCustomersList
{
    public class GetCustomersListQuery : IRequest<CustomerListVm>
    { }
}
