using FluentValidation;
using System.Net;

namespace dotnet7_new_features.EndpointFilters
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            T? argumentToValidate = context.GetArgument<T>(0);
            IValidator<T>? validator = context.HttpContext.RequestServices.GetServices<IValidator<T>>().FirstOrDefault();

            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(argumentToValidate);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary(), statusCode: (int)HttpStatusCode.UnprocessableEntity);

                }
            }

            return await next.Invoke(context);
        }
    }
}
