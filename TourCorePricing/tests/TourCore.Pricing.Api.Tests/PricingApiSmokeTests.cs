using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TourCore.Pricing.Api.Tests;

[TestClass]
public sealed class PricingApiSmokeTests
{
    [TestMethod]
    public async Task Liveness_endpoint_is_anonymous_and_healthy()
    {
        await using var factory = new PricingApiFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/health/live");

        Assert.IsTrue(response.IsSuccessStatusCode);
    }

    private sealed class PricingApiFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((_, configuration) => configuration.AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ConnectionStrings:TourCoreDb"] = "Host=localhost;Database=tourcore_pricing_tests;Username=test;Password=test",
                    ["Authentication:Jwt:Issuer"] = "TourCore.Tests",
                    ["Authentication:Jwt:Audience"] = "TourCore.Pricing.Api",
                    ["Authentication:Jwt:SigningKey"] = "TourCore.Pricing.Tests.SigningKey.NotForProduction.2026",
                    ["Authentication:Jwt:RequireHttpsMetadata"] = "false"
                }));
        }
    }
}
