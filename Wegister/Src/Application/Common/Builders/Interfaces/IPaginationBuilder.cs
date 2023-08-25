using Application.Common.Dtos;
using Application.Common.Viewmodels;

namespace Application.Common.Builders.Interfaces
{
    public interface IPaginationBuilder
    {
        public PaginationVm Build(PaginationDto pagination, int total);
    }
}
