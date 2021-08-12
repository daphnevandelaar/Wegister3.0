using System.Collections.Generic;
using Application.Common.Viewmodels;

namespace WegisterUI.Services
{
    public class ItemService
    {
        private static readonly List<ItemVm> Summaries = new ()
        {
           new ItemVm
           {
               Id = 1,
               Name = "Ultra wide screen",
               Price = 1090m,
               Unit = "Per stuk"
           }
        };

        public List<ItemVm> GetItems()
        {
            return Summaries;
        }

        public void AddItem(ItemVm item)
        {
            Summaries.Add(item);
        }
    }
}
