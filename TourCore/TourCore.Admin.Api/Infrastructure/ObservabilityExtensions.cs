using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace TourCore.Admin.Api.Infrastructure;

public static class ObservabilityExtensions
{
    public static WebApplicationBuilder AddTourCoreObservability(this WebApplicationBuilder builder)
    {
        const string serviceName = "TourCore.Admin.Api";

        builder.Logging.ClearProviders();
        builder.Logging.Configure(options =>
            options.ActivityTrackingOptions = ActivityTrackingOptions.TraceId |
                                              ActivityTrackingOptions.SpanId |
                                              ActivityTrackingOptions.ParentId |
                                              ActivityTrackingOptions.Tags |
                                              ActivityTrackingOptions.Baggage);
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
            options.ParseStateValues = true;
            if (Uri.TryCreate(otlpEndpoint, UriKind.Absolute, out var endpoint))
                options.AddOtlpExporter(exporter => exporter.Endpoint = endpoint);
        });

        var telemetry = builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(
                serviceName,
                serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString()));

        telemetry.WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation(options =>
                    options.Filter = context => !context.Request.Path.StartsWithSegments("/health"))
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation();

            if (Uri.TryCreate(otlpEndpoint, UriKind.Absolute, out var endpoint))
                tracing.AddOtlpExporter(exporter => exporter.Endpoint = endpoint);
        });

        telemetry.WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation();

            if (Uri.TryCreate(otlpEndpoint, UriKind.Absolute, out var endpoint))
                metrics.AddOtlpExporter(exporter => exporter.Endpoint = endpoint);
        });

        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" })
            .AddCheck<PostgresReadinessHealthCheck>("postgres", tags: new[] { "ready" });

        return builder;
    }

    public static WebApplication MapTourCoreHealthChecks(this WebApplication app)
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
        var response = new
        {
            status = report.Status.ToString(),
            durationMs = Math.Round(report.TotalDuration.TotalMilliseconds, 2),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                durationMs = Math.Round(entry.Value.Duration.TotalMilliseconds, 2),
                description = entry.Value.Description
            })
        };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
