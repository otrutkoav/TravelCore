using System;
using System.Linq;
using System.Net;
using TourCore.Application.Common.Exceptions;

namespace TourCore.Api.Errors
{
    public static class ApiExceptionMapper
    {
        public static ApiExceptionMapping Map(Exception exception, Func<string, string> validationMessageResolver = null)
        {
            var validationException = exception as ValidationException;
            if (validationException != null)
            {
                return new ApiExceptionMapping(HttpStatusCode.BadRequest, new ApiErrorResponse
                {
                    Code = validationException.Code,
                    Message = validationException.Message,
                    Details = validationException.Errors.ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value.Select(error => new ValidationErrorItem
                        {
                            Code = error.Code,
                            Message = error.Message ?? validationMessageResolver?.Invoke(error.Code) ?? error.Code
                        }).ToArray())
                });
            }

            var notFoundException = exception as NotFoundException;
            if (notFoundException != null)
                return Error(HttpStatusCode.NotFound, notFoundException.Code, notFoundException.Message);

            var conflictException = exception as ConflictException;
            if (conflictException != null)
                return Error(HttpStatusCode.Conflict, conflictException.Code, conflictException.Message);

            return Error(HttpStatusCode.InternalServerError, "internal_error", "Внутренняя ошибка сервера.");
        }

        public static ApiExceptionMapping Error(HttpStatusCode statusCode, string code, string message)
        {
            return new ApiExceptionMapping(statusCode, new ApiErrorResponse { Code = code, Message = message });
        }
    }

    public sealed class ApiExceptionMapping
    {
        public ApiExceptionMapping(HttpStatusCode statusCode, ApiErrorResponse response)
        {
            StatusCode = statusCode;
            Response = response;
        }

        public HttpStatusCode StatusCode { get; }
        public ApiErrorResponse Response { get; }
    }
}
