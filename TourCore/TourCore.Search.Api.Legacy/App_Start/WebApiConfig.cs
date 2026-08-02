using System.Web.Http;
using TourCore.Search.Api.Legacy.Filters;

namespace TourCore.Search.Api.Legacy
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Filters.Add(new ApiExceptionFilterAttribute());
            config.MapHttpAttributeRoutes();
        }
    }
}
