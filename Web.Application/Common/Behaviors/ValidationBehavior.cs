using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Application.Common.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;

            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Any())
                {
                    _logger.LogError($"Validation failed: {JsonConvert.SerializeObject(request)}");

                    throw new ValidationException(failures);
                }
                    
            }
            return await next();
        }

        private string ErrorMessage(List<ValidationFailure> failures)
        {
            var messageFormat = "Validation failed:\r\n {0}";

            var errorFormat = "- {0}: {1}\r\n";

            var errors = string.Empty;

            foreach (var failure in failures)
            {
                errors += string.Format(errorFormat, failure.PropertyName, failure.ErrorMessage);
            }

            var message = string.Format(messageFormat, errors);

            return message;
        }
    }
}
