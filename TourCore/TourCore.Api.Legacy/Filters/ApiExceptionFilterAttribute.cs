#pragma warning disable 1591

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using TourCore.Api.Legacy.Models;
using TourCore.Application.Common.Errors;
using TourCore.Application.Common.Exceptions;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Api.Legacy.Filters
{
    /// <summary>
    /// Глобальный фильтр обработки исключений API.
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            if (exception is ValidationException validationException)
            {
                context.Response = CreateResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    validationException.Code,
                    validationException.Message,
                    MapValidationErrors(validationException.Errors));

                return;
            }

            if (exception is NotFoundException notFoundException)
            {
                context.Response = CreateResponse(
                    context,
                    HttpStatusCode.NotFound,
                    notFoundException.Code,
                    notFoundException.Message);

                return;
            }

            if (exception is ConflictException conflictException)
            {
                context.Response = CreateResponse(
                    context,
                    HttpStatusCode.Conflict,
                    conflictException.Code,
                    conflictException.Message);

                return;
            }

            if (exception is DomainException)
            {
                context.Response = CreateResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    ErrorCode.DomainError,
                    exception.Message);

                return;
            }

            context.Response = CreateResponse(
                context,
                HttpStatusCode.InternalServerError,
                ErrorCode.InternalError,
                ErrorMessages.InternalError);
        }

        private static HttpResponseMessage CreateResponse(
            HttpActionExecutedContext context,
            HttpStatusCode statusCode,
            string code,
            string message,
            object details = null)
        {
            var error = new ApiErrorResponse
            {
                Code = code,
                Message = message,
                Details = details
            };

            return context.Request.CreateResponse(statusCode, error);
        }

        private static object MapValidationErrors(IReadOnlyDictionary<string, string[]> errors)
        {
            if (errors == null)
                return null;

            return errors.ToDictionary(
                x => x.Key,
                x => x.Value
                    .Select(code => new ValidationErrorItem
                    {
                        Code = code,
                        Message = ValidationErrorMessageResolver.Resolve(code)
                    })
                    .ToArray());
        }
    }

    /// <summary>
    /// Ответ API при ошибке.
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        /// Машинный код ошибки.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Сообщение об ошибке.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дополнительные детали ошибки.
        /// </summary>
        public object Details { get; set; }
    }
}

#pragma warning restore 1591