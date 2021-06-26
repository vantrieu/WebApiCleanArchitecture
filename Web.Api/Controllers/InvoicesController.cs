using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Application.Common.Interfaces;
using Web.Application.Invoices.Commands;
using Web.Application.Invoices.Queries;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class InvoicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ICurrentUserService _currentUserService;

        public InvoicesController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;

            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var result = await _mediator.Send(new GetUserInvoicesQuery { UserId = _currentUserService.UserId });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand command)
        {
            var result = await _mediator.Send(command);

            return Created(string.Empty, result);
        }
    }
}
