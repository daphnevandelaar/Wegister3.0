using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Employers.Commands.CreateEmployer
{
    public class EmployerCreatedHandler : INotificationHandler<EmployerCreated>
    {
        private readonly INotificationService _notification;

        public EmployerCreatedHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(EmployerCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new MessageDto());
        }
    }
}
