using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Application.Invoices.Commands;
using Web.Application.Invoices.ViewModels;
using Web.Domain.Entities;

namespace Web.Application.Invoices.Handlers
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceVm>
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }

        public async Task<InvoiceVm> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Invoice>(request);

            _context.Invoices.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<InvoiceVm>(entity);
        }
    }
}
