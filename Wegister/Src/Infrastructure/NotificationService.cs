using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;

namespace Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}
