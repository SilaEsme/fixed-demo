using FixedDemo.Domain.Primitives;
using FixedDemo.Domain.Wrapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace FixedDemo.Application.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse?>
     where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators;
        }

        public async Task<TResponse?> Handle(TRequest request, RequestHandlerDelegate<TResponse?> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                return await next.Invoke();
            }

            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count == 0)
            {
                return await next.Invoke();
            }

            var response = CreateValidationErrorResponse(failures);
            return response;

        }

        private TResponse? CreateValidationErrorResponse(IEnumerable<ValidationFailure> failures)
        {
            var errors = failures.Select(failure => failure.ErrorMessage).ToList();

            return (TResponse?)(typeof(ApiResult<NoContent>).MakeGenericType(typeof(TResponse).GetGenericArguments())
            .GetMethod("ValidationError")?.Invoke(null, [errors]));
        }
    }

}
