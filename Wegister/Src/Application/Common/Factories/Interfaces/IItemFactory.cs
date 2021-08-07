using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces.Abstracts;
using Application.Common.Viewmodels;
using Application.Items.Commands.CreateItem;
using Domain.Entities;

namespace Application.Common.Factories.Interfaces
{
    public interface IItemFactory : 
        IFactory<CreateItemCommand, Item>, 
        IFactory<Item, ItemCreated>, 
        IFactory<List<ItemLookupDto>, ItemListVm>
    {
        public ItemLookupDto CreateLookUpDto(Item item);
    }
}
