using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommand : IRequest
    {
        public CreateItemCommand(string name, decimal price, string unit)
        {
            Name = name;
            Price = price;
            Unit = unit;
        }

        public string Name { get; }
        public decimal Price { get; }
        public string Unit { get; }
    }
}
