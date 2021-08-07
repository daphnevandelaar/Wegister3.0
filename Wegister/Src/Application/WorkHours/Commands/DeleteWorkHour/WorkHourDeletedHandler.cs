using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using MediatR;

namespace Application.WorkHours.Commands.DeleteWorkHour
{
    public class WorkHourDeletedHandler : INotificationHandler<WorkHourDeleted>
    {
        private readonly INotificationService _notification;

        public WorkHourDeletedHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(WorkHourDeleted notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new MessageDto());
        }
    }
}
