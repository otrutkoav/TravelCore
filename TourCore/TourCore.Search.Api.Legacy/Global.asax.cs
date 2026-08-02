using System.Web.Http;

namespace TourCore.Search.Api.Legacy
{
    public class SearchApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
            SwaggerConfig.Register();
        }

        protected void Application_BeginRequest()
        {
            RewriteLegacyApiPath();
        }

        private void RewriteLegacyApiPath()
        {
            var path = Request.Path;
            if (!path.StartsWith("/api/", System.StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith("/api/v1/", System.StringComparison.OrdinalIgnoreCase))
                return;

            Context.RewritePath(
                "/api/v1" + path.Substring(4),
                null,
                Request.Url.Query.TrimStart('?'),
                false);
        }
    }
}
