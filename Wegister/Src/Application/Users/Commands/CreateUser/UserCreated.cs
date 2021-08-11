using MediatR;

namespace Application.Users.Commands.CreateUser
{
    public class UserCreated : INotification
    {
        public int Id { get; }

        public UserCreated(int id)
        {
            Id = id;
        }
    }
}
