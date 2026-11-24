using TourCore.Pricing.Application.Common;
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
                DomainException domainException => (StatusCodes.Status400BadRequest, new { Code = "domain_error", Message = domainException.Message, Details = (object?)null }),
                ValidationException validationException => (StatusCodes.Status400BadRequest, new { Code = validationException.Code, Message = validationException.Message, Details = (object?)validationException.Errors }),
                NotFoundException notFoundException => (StatusCodes.Status404NotFound, new { Code = notFoundException.Code, Message = notFoundException.Message, Details = (object?)null }),
                _ => (StatusCodes.Status500InternalServerError, new { Code = "internal_error", Message = "Внутренняя ошибка сервера.", Details = (object?)null })
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
                _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}.", context.Request.Method, context.Request.Path);

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
