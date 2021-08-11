using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using Application.WorkHours.Commands.CreateWorkHour;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class UserCreatedHandler : INotificationHandler<WorkHourCreated>
    {
        private readonly INotificationService _notification;

        public UserCreatedHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(WorkHourCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new MessageDto());
        }
    }
}
