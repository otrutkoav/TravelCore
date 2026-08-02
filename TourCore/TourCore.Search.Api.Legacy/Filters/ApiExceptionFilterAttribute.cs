using System.Net.Http;
using System.Web.Http.Filters;
using TourCore.Api.Errors;

namespace TourCore.Search.Api.Legacy.Filters
{
    public sealed class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var mapping = ApiExceptionMapper.Map(context.Exception);
            context.Response = context.Request.CreateResponse(mapping.StatusCode, mapping.Response);
        }
    }
}
