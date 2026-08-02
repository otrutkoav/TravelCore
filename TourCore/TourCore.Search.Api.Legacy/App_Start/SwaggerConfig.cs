using System.Web.Http;
using Swashbuckle.Application;
using System;
using System.IO;

namespace TourCore.Search.Api.Legacy
{
    public static class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(configuration =>
                {
                    configuration.SingleApiVersion("v1", "TourCore Search API");
                    configuration.DescribeAllEnumsAsStrings();

                    var binDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
                    var xmlComments = new[]
                    {
                        "TourCore.Search.Api.Legacy.xml",
                        "TourCore.Search.Contracts.xml"
                    };

                    foreach (var xmlComment in xmlComments)
                    {
                        var xmlPath = Path.Combine(binDirectory, xmlComment);
                        if (File.Exists(xmlPath))
                            configuration.IncludeXmlComments(xmlPath);
                    }
                })
                .EnableSwaggerUi();
        }
    }
}
