using MediatR;

namespace Application.Employers.Commands.CreateEmployer
{
    public class EmployerCreated : INotification
    {
        public int Id { get; }

        public EmployerCreated(int id)
        {
            Id = id;
        }
    }
}
