using MediatR;
using System.Collections.Generic;
using Web.Application.Invoices.ViewModels;

namespace Web.Application.Invoices.Queries
{
    public class GetUserInvoicesQuery : IRequest<IEnumerable<InvoiceVm>>
    {
        public string UserId { get; set; }
    }
}
