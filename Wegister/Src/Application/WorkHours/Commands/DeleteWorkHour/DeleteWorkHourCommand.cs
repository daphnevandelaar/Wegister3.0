
using MediatR;

namespace Application.WorkHours.Commands.DeleteWorkHour
{
    public class DeleteWorkHourCommand : IRequest
    {
        public int Id { get; }

        public DeleteWorkHourCommand(int id)
        {
            Id = id;
        }
    }
}