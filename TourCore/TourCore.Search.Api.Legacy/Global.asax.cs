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
    }
}
