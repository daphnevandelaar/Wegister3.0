﻿using System.Threading;
using System.Threading.Tasks;
using Application.Common.Dtos;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Customers.Commands.CreateCustomer
{
    public class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
    {
        private readonly INotificationService _notification;

        public CustomerCreatedHandler(INotificationService notification)
        {
            _notification = notification;
        }

        public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
        {
            await _notification.SendAsync(new MessageDto());
        }
    }
}
