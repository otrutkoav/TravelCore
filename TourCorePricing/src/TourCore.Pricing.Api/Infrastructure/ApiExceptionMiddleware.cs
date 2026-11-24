using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Contracts.Common;
using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Api.Infrastructure;

internal sealed class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ApiExceptionMiddleware> _logger;

    public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            var (statusCode, response) = exception switch
            {
                DomainException domainException => (StatusCodes.Status400BadRequest, Error("domain_error", domainException.Message)),
                ValidationException validationException => (StatusCodes.Status400BadRequest, Error(validationException.Code, validationException.Message, validationException.Errors)),
                NotFoundException notFoundException => (StatusCodes.Status404NotFound, Error(notFoundException.Code, notFoundException.Message)),
                _ => (StatusCodes.Status500InternalServerError, Error("internal_error", "Внутренняя ошибка сервера."))
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}.", context.Request.Method, context.Request.Path);

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }

    private static PricingApiErrorDto Error(
        string code,
        string message,
        IReadOnlyDictionary<string, string[]>? details = null) =>
        new() { Code = code, Message = message, Details = details };
}
