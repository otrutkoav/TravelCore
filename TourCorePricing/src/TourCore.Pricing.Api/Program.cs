using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Api.Infrastructure;
using TourCore.Pricing.Application;
using TourCore.Pricing.Infrastructure.Postgres;
using TourCore.Pricing.Infrastructure.Postgres.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.AddPricingObservability();
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddHttpContextAccessor();
builder.Services.AddPricingAuthorization(builder.Configuration);
builder.Services.AddPricingApplication();
builder.Services.AddPricingApiServices();

var connectionString = builder.Configuration.GetConnectionString("TourCoreDb")
    ?? throw new InvalidOperationException("Connection string 'TourCoreDb' is not configured.");
builder.Services.AddPricingPostgresInfrastructure(connectionString);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "TourCore Pricing API", Version = "v1" });
    options.CustomSchemaIds(type => type.FullName?.Replace('+', '.') ?? type.Name);
    options.AddPricingJwtSecurity(builder.Configuration);
    foreach (var assemblyName in new[]
             {
                 Assembly.GetExecutingAssembly().GetName().Name,
                 "TourCore.Pricing.Application",
                 "TourCore.Pricing.Contracts"
             })
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
        if (File.Exists(xmlPath))
            options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

if (args.Any(argument => string.Equals(argument, "--migrate-only", StringComparison.OrdinalIgnoreCase)))
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<PricingDbContext>();
    dbContext.Database.Migrate();
    return;
}

app.UseMiddleware<ApiExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapPricingHealthChecks();
app.MapControllers();
app.Run();

public partial class Program;
