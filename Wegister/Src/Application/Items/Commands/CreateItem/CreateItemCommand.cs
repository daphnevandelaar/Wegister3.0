using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class CreateItemCommand : IRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
    }
}
