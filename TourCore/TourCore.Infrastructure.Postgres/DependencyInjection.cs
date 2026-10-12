using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TourCore.Infrastructure.Postgres.Common;
using TourCore.Infrastructure.Postgres.Persistence;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Common.Data;

namespace TourCore.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPostgresInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<TourCoreDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IQuerySpecificationExecutor, EfCoreQuerySpecificationExecutor>();

        var repositoryInterfaceNamespace = "TourCore.Application.Abstractions.Persistence";
        var repositoryTypes = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsClass: true })
            .Select(type => new
            {
                Implementation = type,
                Services = type.GetInterfaces()
                    .Where(service => service.Namespace?.StartsWith(repositoryInterfaceNamespace, StringComparison.Ordinal) == true)
                    .ToArray()
            })
            .Where(registration => registration.Services.Length > 0);

        foreach (var registration in repositoryTypes)
        foreach (var service in registration.Services)
            services.AddScoped(service, registration.Implementation);

        return services;
    }
}
