using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Application.Invoices.Commands;
using Web.Domain.Entities;

namespace Web.Application.Invoices.Handlers
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateInvoiceCommandHandler(IApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Invoice
            {
                AmountPaid = request.AmountPaid,
                Date = request.Date,
                Discount = request.Discount,
                DiscountType = request.DiscountType,
                DueDate = request.DueDate,
                From = request.From,
                InvoiceNumber = request.InvoiceNumber,
                Logo = request.Logo,
                PaymentTerms = request.PaymentTerms,
                Tax = request.Tax,
                TaxType = request.TaxType,
                To = request.To,
                InvoiceItems = request.InvoiceItems.Select(x => new InvoiceItem
                {
                    Item = x.Item,
                    Quantity = x.Quantity,
                    Rate = x.Rate
                }).ToList()
            };

            _context.Invoices.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
