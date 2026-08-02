
using System.Reflection;
using TourCore.Admin.Api.Infrastructure;
using TourCore.Infrastructure.Postgres;

namespace TourCore.Admin.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("TourCoreDb")
                ?? throw new InvalidOperationException("Connection string 'TourCoreDb' is not configured.");

            builder.Services.AddControllers().AddJsonOptions(options =>
                options.JsonSerializerOptions.PropertyNamingPolicy = null);
            builder.Services.AddTourCoreApplication();
            builder.Services.AddPostgresInfrastructure(connectionString);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new() { Title = "TourCore Admin API", Version = "v1" });
                options.CustomSchemaIds(type => type.FullName?.Replace('+', '.') ?? type.Name);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            app.UseMiddleware<ApiExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
