using Microsoft.OpenApi.Models;
using TourCore.Identity.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole(options =>
{
    options.IncludeScopes = true;
    options.UseUtcTimestamp = true;
    options.TimestampFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
});

builder.Services.AddIdentityTokenIssuer(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TourCore Identity API",
        Version = "v1",
        Description = "Local identity provider for TourCore development and the first vertical slice."
    });
});
builder.Services.AddHealthChecks();
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];
if (allowedOrigins.Length == 0 || allowedOrigins.Any(string.IsNullOrWhiteSpace))
    throw new InvalidOperationException("Cors:AllowedOrigins must contain at least one trusted origin.");
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
    .WithOrigins(allowedOrigins)
    .AllowAnyHeader()
    .AllowAnyMethod()));

var app = builder.Build();
app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health/live").AllowAnonymous();
app.MapControllers();
app.Run();

public partial class Program;
