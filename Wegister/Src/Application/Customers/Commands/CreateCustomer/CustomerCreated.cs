using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CustomerCreated : INotification
    {
        public int Id { get; }
        public CustomerCreated(int id)
        {
            Id = id;
        }
    }
}
