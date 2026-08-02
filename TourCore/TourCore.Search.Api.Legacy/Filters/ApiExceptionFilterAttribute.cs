using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using TourCore.Search.Api.Legacy.Models;
using TourCore.Search.Application.Exceptions;

namespace TourCore.Search.Api.Legacy.Filters
{
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var validationException = context.Exception as TourSearchValidationException;
            if (validationException == null)
                return;

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new SearchValidationErrorResponse(validationException.Message, validationException.Errors));
        }
    }
}
