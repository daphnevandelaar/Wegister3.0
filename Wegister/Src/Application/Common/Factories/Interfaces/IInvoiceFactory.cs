using System;
using System.Linq.Expressions;
using Application.Common.Dtos;
using Domain.Entities;

namespace Application.Common.Factories.Interfaces
{
    public interface IInvoiceFactory
    {
        Expression<Func<Invoice, InvoiceDto>> CreateDto { get; }
    }
}
