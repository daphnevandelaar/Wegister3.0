using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Viewmodels;

namespace Application.Common.Factories.Interfaces
{
    public interface ICommonFactory
    {
        public SearchVm CreateCommon(SearchDto entity);
        public List<SearchVm> CreateCommon(List<SearchDto> entity);
    }
}
