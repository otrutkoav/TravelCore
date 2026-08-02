#pragma warning disable 1591

using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using TourCore.Api.Errors;
using TourCore.Application.Common.Errors;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Admin.Api.Legacy.Filters
{
    /// <summary>
    /// Глобальный фильтр обработки исключений API.
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            if (exception is DomainException)
            {
                context.Response = CreateResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    ErrorCode.DomainError,
                    exception.Message);

                return;
            }

            var mapping = ApiExceptionMapper.Map(exception, ValidationErrorMessageResolver.Resolve);
            context.Response = context.Request.CreateResponse(mapping.StatusCode, mapping.Response);
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

}

#pragma warning restore 1591
