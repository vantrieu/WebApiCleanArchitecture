using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Application.Invoices.Queries;
using Web.Application.Invoices.ViewModels;

namespace Web.Application.Invoices.Handlers
{
    public class GetUserInvoicesQueryHandler : IRequestHandler<GetUserInvoicesQuery, IEnumerable<InvoiceVm>>
    {
        private readonly IApplicationDbContext _context;

        public GetUserInvoicesQueryHandler(IApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<InvoiceVm>> Handle(GetUserInvoicesQuery request, CancellationToken cancellationToken)
        {
            var result = new List<InvoiceVm>();
            var invoices = await _context.Invoices.Include(i => i.InvoiceItems).Where(i => i.CreatedBy.Equals(request.UserId)).ToListAsync();
            if(invoices != null)
            {
                result = invoices.Select(i => new InvoiceVm
                {
                    AmountPaid = i.AmountPaid,
                    Date = i.Date,
                    Discount = i.Discount,
                    DiscountType = i.DiscountType,
                    DueDate = i.DueDate,
                    From = i.From,
                    InvoiceNumber = i.InvoiceNumber,
                    Logo = i.Logo,
                    PaymentTerms = i.PaymentTerms,
                    Tax = i.Tax,
                    TaxType = i.TaxType,
                    To = i.To,
                    InvoiceItems = i.InvoiceItems.Select(x => new InvoiceItemVm
                    {
                        Id = x.Id,
                        Item = x.Item,
                        Quantity = x.Quantity,
                        Rate = x.Rate
                    }).ToList(),
                    CreatedAt = i.CreatedAt
                }).ToList();
            }
            return result;
        }
    }
}
