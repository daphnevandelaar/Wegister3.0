using System.Collections.Generic;
using Application.Common.Dtos;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Application.Items.Commands.CreateItem;
using Domain.Entities;

namespace Application.Common.Factories
{
    public class ItemFactory : FactoryBase, IItemFactory
    {
        public static ItemListVm Create()
        {
            return new ItemListVm()
            {
            };
        }

        public ItemListVm Create(List<ItemLookupDto> entity)
        {
            var returnValue = Create();

            if (entity != null)
            {
                returnValue = null;
            }

            return returnValue;
        }

        ItemLookupDto IItemFactory.CreateLookUpDto(Item item)
        {
            return new ItemLookupDto()
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price
            };
        }

        public Item Create(CreateItemCommand entity)
        {
            var returnValue = new Item();

            if (entity != null)
            {
                returnValue.Name = entity.Name;
                returnValue.Price = entity.Price;
                returnValue.Unit = entity.Unit;
            }

            return returnValue;
        }

        public ItemCreated Create(Item entity)
        {
            if (entity != null)
                return new ItemCreated(entity.Id);

            return null;
        }
    }
}
