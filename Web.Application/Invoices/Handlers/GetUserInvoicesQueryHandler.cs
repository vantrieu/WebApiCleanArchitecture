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

        private readonly ICurrentUserService _currentUserService;

        public GetUserInvoicesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;

            _mapper = mapper;

            _currentUserService = currentUserService;
        }
        public async Task<IEnumerable<InvoiceVm>> Handle(GetUserInvoicesQuery request, CancellationToken cancellationToken)
        {
            var result = new List<InvoiceVm>();
            var invoices = await _context.Invoices.Where(i => i.CreatedBy == _currentUserService.UserId.ToString())
                .Include(i => i.InvoiceItems).ToListAsync();
            if(invoices != null)
            {
                result = _mapper.Map<List<InvoiceVm>>(invoices);
            }
            return result;
        }
    }
}
