using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class WorkHourCreatedHandler : INotificationHandler<WorkHourCreated>
    {
        private readonly INotificationService _notification;

        public WorkHourCreatedHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(WorkHourCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new MessageDto());
        }
    }
}
