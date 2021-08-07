using MediatR;

namespace Application.WorkHours.Commands.DeleteWorkHour
{
    public class WorkHourDeleted : INotification
    {
        public int Id { get; }

        public WorkHourDeleted(int id)
        {
            Id = id;
        }
    }
}
