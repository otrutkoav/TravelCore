using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
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
                    "validation_error",
                    "Ошибка валидации.",
                    validationException.Errors);

                return;
            }

            if (exception is NotFoundException notFoundException)
            {
                context.Response = CreateResponse(
                    context,
                    HttpStatusCode.NotFound,
                    notFoundException.Code ?? "not_found",
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
                    "domain_error",
                    exception.Message);

                return;
            }

            context.Response = CreateResponse(
                context,
                HttpStatusCode.InternalServerError,
                "internal_error",
                "Внутренняя ошибка сервера.");
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