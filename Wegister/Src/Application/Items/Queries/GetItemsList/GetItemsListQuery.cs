using Application.Common.Viewmodels;
using MediatR;

namespace Application.Items.Queries.GetItemsList
{
    public class GetItemsListQuery : IRequest<ItemListVm>
    {
    }
}
