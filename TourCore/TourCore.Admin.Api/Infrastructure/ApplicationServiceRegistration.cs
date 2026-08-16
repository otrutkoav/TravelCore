using Microsoft.AspNetCore.Mvc;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Caching;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Queries;
using TourCore.Application.Tours;

namespace TourCore.Admin.Api.Infrastructure;

internal static class ApplicationServiceRegistration
{
    public static IServiceCollection AddTourCoreApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(PagedQueryExecutor).Assembly;
        var handlerDefinitions = new[] { typeof(ICommandHandler<,>), typeof(IQueryHandler<,>) };
        var applicationTypes = applicationAssembly.GetTypes();
        var requiredHandlers = typeof(ApplicationServiceRegistration)
            .Assembly.GetTypes()
            .Where(type => !type.IsAbstract && typeof(ControllerBase).IsAssignableFrom(type))
            .SelectMany(type => type.GetConstructors())
            .SelectMany(constructor => constructor.GetParameters())
            .Select(parameter => parameter.ParameterType)
            .Where(type =>
                type.IsGenericType && handlerDefinitions.Contains(type.GetGenericTypeDefinition())
            )
            .Distinct()
            .ToArray();

        foreach (var service in requiredHandlers)
        {
            var implementations = applicationTypes
                .Where(type =>
                    type is { IsClass: true, IsAbstract: false }
                    && type.GetInterfaces().Contains(service)
                )
                .ToArray();

            if (implementations.Length != 1)
                throw new InvalidOperationException(
                    $"Expected exactly one implementation of {service.FullName}, found "
                        + $"{implementations.Length}: {string.Join(", ", implementations.Select(x => x.FullName))}."
                );

            services.AddScoped(service, implementations[0]);
        }

        foreach (
            var validator in applicationTypes.Where(type =>
                type is { IsClass: true, IsAbstract: false }
                && type.Namespace?.EndsWith(".Validators", StringComparison.Ordinal) == true
            )
        )
            services.AddScoped(validator);

        services.AddScoped<PagedQueryExecutor>();
        services.AddScoped<TourTypeCommandValidator>();
        services.AddScoped<TourCommandValidator>();
        services.AddScoped<TourServiceCommandValidator>();
        services.AddScoped<TourFlightCommandValidator>();
        services.AddScoped<TourHotelCommandValidator>();
        services.AddScoped<TourBusTransferCommandValidator>();
        services.AddScoped<TourRailwayTransferCommandValidator>();
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddSingleton<NoCacheCatalogServices>();
        services.AddSingleton<ICatalogCacheProvider>(provider =>
            provider.GetRequiredService<NoCacheCatalogServices>()
        );
        services.AddSingleton<ICatalogCacheUpdater>(provider =>
            provider.GetRequiredService<NoCacheCatalogServices>()
        );
        services.AddSingleton<ICatalogCacheInvalidator>(provider =>
            provider.GetRequiredService<NoCacheCatalogServices>()
        );
        return services;
    }
}
