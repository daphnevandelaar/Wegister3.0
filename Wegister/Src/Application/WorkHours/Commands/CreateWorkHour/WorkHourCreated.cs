using MediatR;

namespace Application.WorkHours.Commands.CreateWorkHour
{
    public class WorkHourCreated : INotification
    {
        public int Id { get; }

        public WorkHourCreated(int id)
        {
            Id = id;
        }
    }
}
