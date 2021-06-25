using FluentValidation;
using System.Linq;
using Web.Application.Invoices.Commands;

namespace Web.Application.Invoices.Validators
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            RuleFor(x => x.AmountPaid).NotNull();

            RuleFor(x => x.Date).NotNull();

            RuleFor(x => x.From).NotEmpty().MinimumLength(3);

            RuleFor(x => x.To).NotEmpty().MinimumLength(3);

            RuleFor(x => x.InvoiceItems).Must(collection => collection != null && collection
                .Any()).WithMessage("Property InvoiceItems should not be an empty list.");
        }
    }
}
