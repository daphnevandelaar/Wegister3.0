using Application.Common.Viewmodels;
using MediatR;

namespace Application.Employers.Queries.GetEmployersList
{
    public class GetEmployersListQuery : IRequest<EmployerListVm>
    { }
}
