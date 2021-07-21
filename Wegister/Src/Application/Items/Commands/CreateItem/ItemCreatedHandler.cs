using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Items.Commands.CreateItem
{
    public class ItemCreatedHandler : INotificationHandler<ItemCreated>
    {
        private readonly INotificationService _notification;

        public ItemCreatedHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(ItemCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new MessageDto());
        }
    }
}
