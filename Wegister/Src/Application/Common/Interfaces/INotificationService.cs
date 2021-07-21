using System.Threading.Tasks;
using Application.Common.Dtos;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(MessageDto message);
    }
}
