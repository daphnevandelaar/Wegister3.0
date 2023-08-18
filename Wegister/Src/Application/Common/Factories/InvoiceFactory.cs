using Application.Common.Dtos;
using System.Linq.Expressions;
using System;
using Application.Common.Factories.Interfaces;
using Domain.Entities;

namespace Application.Common.Factories
{
    public class InvoiceFactory : IInvoiceFactory
    {
        public Expression<Func<Invoice, InvoiceDto>> CreateDto = invoice => new() { };

        Expression<Func<Invoice, InvoiceDto>> IInvoiceFactory.CreateDto => throw new NotImplementedException();
    }
}
