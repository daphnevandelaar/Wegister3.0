using Application.Common.Dtos;
using Application.Common.Viewmodels;

namespace Application.Common.Factories.Interfaces
{
    public interface IPaginationFactory
    {   
        public PaginationDto GetPaginationDto(PaginationVm pagination);
    }
}
