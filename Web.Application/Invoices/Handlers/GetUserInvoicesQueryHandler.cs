using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        private readonly IMapper _mapper;

        public GetUserInvoicesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }
        public async Task<IEnumerable<InvoiceVm>> Handle(GetUserInvoicesQuery request, CancellationToken cancellationToken)
        {
            var result = new List<InvoiceVm>();
            var invoices = await _context.Invoices.Include(i => i.InvoiceItems).Where(i => i.CreatedBy.Equals(request.UserId)).ToListAsync();
            if(invoices != null)
            {
                result = _mapper.Map<List<InvoiceVm>>(invoices);
            }
            return result;
        }
    }
}
