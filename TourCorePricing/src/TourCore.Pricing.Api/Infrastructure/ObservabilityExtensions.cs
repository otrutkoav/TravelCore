using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TourCore.Pricing.Api.Infrastructure;

internal static class ObservabilityExtensions
{
    public static WebApplicationBuilder AddPricingObservability(this WebApplicationBuilder builder)
    {
        const string serviceName = "TourCore.Pricing.Api";
        builder.Logging.ClearProviders();
        builder.Logging.Configure(options => options.ActivityTrackingOptions =
            ActivityTrackingOptions.TraceId | ActivityTrackingOptions.SpanId | ActivityTrackingOptions.ParentId);
        builder.Logging.AddJsonConsole(options =>
        {
            options.IncludeScopes = true;
            options.UseUtcTimestamp = true;
            options.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
        });

        var otlpEndpoint = builder.Configuration["OpenTelemetry:OtlpEndpoint"];
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeFormattedMessage = true;
            options.IncludeScopes = true;
            if (Uri.TryCreate(otlpEndpoint, UriKind.Absolute, out var endpoint))
                options.AddOtlpExporter(exporter => exporter.Endpoint = endpoint);
        });

        var telemetry = builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName));
        telemetry.WithTracing(tracing =>
        {
            tracing.AddAspNetCoreInstrumentation(options =>
                    options.Filter = context => !context.Request.Path.StartsWithSegments("/health"))
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation();
            if (Uri.TryCreate(otlpEndpoint, UriKind.Absolute, out var endpoint))
                tracing.AddOtlpExporter(exporter => exporter.Endpoint = endpoint);
        });
        telemetry.WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation().AddHttpClientInstrumentation().AddRuntimeInstrumentation();
            if (Uri.TryCreate(otlpEndpoint, UriKind.Absolute, out var endpoint))
                metrics.AddOtlpExporter(exporter => exporter.Endpoint = endpoint);
        });

        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
            .AddCheck<PricingPostgresReadinessHealthCheck>("postgres", tags: new[] { "ready" });
        return builder;
    }

    public static WebApplication MapPricingHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = registration => registration.Tags.Contains("live"),
            ResponseWriter = WriteResponseAsync
        }).AllowAnonymous();
        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = registration => registration.Tags.Contains("ready"),
            ResponseWriter = WriteResponseAsync
        }).AllowAnonymous();
        return app;
    }

    private static Task WriteResponseAsync(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            durationMs = Math.Round(report.TotalDuration.TotalMilliseconds, 2),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description
            })
        }));
    }
}
