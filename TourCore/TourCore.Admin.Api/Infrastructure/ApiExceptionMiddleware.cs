using System.Net;
using TourCore.Api.Errors;
using TourCore.Application.Common.Errors;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Admin.Api.Infrastructure;

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
            var mapping = exception is DomainException
                ? ApiExceptionMapper.Error(HttpStatusCode.BadRequest, ErrorCode.DomainError, exception.Message)
                : ApiExceptionMapper.Map(exception, ValidationErrorMessageResolver.Resolve);

            if (mapping.StatusCode == HttpStatusCode.InternalServerError)
                _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}.",
                    context.Request.Method, context.Request.Path);

            context.Response.StatusCode = (int)mapping.StatusCode;
            await context.Response.WriteAsJsonAsync(mapping.Response);
        }
    }
}
