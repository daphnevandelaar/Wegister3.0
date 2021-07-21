using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class ItemCreated : INotification
    {
        public int Id { get; }
        public ItemCreated(int id)
        {
            Id = id;
        }
    }
}
