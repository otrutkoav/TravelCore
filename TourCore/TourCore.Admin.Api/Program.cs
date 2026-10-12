
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TourCore.Admin.Api.Infrastructure;
using TourCore.Infrastructure.Postgres;
using TourCore.Infrastructure.Postgres.Persistence;

namespace TourCore.Admin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddTourCoreObservability();

            var connectionString = builder.Configuration.GetConnectionString("TourCoreDb")
                ?? throw new InvalidOperationException("Connection string 'TourCoreDb' is not configured.");

            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);
            builder.Services.AddTourCoreAuthorization(builder.Configuration);
            builder.Services.AddCatalogAndProductApplication();
            builder.Services.AddPostgresInfrastructure(connectionString);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "TourCore Admin API", Version = "v1" });
                options.CustomSchemaIds(type => type.FullName?.Replace('+', '.') ?? type.Name);
                options.AddTourCoreJwtSecurity(builder.Configuration);
                var xmlComments = new[]
                {
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml",
                    "TourCore.Catalog.Application.xml",
                    "TourCore.Catalog.Contracts.xml",
                    "TourCore.Product.Application.xml",
                    "TourCore.Product.Contracts.xml"
                };
                foreach (var xmlFile in xmlComments)
                {
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
                }
            });

            var app = builder.Build();

            if (args.Any(argument =>
                    string.Equals(argument, "--migrate-only", StringComparison.OrdinalIgnoreCase)))
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<TourCoreDbContext>();
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
            app.MapTourCoreHealthChecks();
            app.MapControllers();

            app.Run();
        }
    }
}
