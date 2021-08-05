using Domain.Entities;
using System.Collections.Generic;

namespace Application.UnitTests.Common.DatabaseSeeders
{
    public class ItemDbSeeder : ISeeder<Item>
    {
        public HashSet<Item> Seed()
        {
            return new()
            {
                new Item
                {
                    Name = "Computer mouse",
                    Price = 35.00m,
                    Unit = "Per piece"
                },
                new Item
                {
                    Name = "USB-stick 5gb",
                    Price = 3.00m,
                    Unit = "Per piece",
                },
                new Item
                {
                    Name = "Installing computer",
                    Price = 100.00m,
                    Unit = "Per hour",
                }
            };
        }
    }
}
