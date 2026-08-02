#pragma warning disable 1591

using System.Web.Http;

namespace TourCore.Admin.Api.Legacy
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
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

#pragma warning restore 1591
