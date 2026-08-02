using System;
using System.Linq;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Search.Application.Abstractions;
using TourCore.Search.Application.Handlers;
using TourCore.Search.Application.Validators;
using TourCore.Search.Contracts;
using TourCore.Infrastructure.SqlServer.Search.Persistence;
using TourCore.Infrastructure.SqlServer.Search.Queries;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace TourCore.Search.Api.Legacy
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<SearchReadDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<ITourSearchDataSource, Ef6TourSearchDataSource>(new HierarchicalLifetimeManager());
            container.RegisterType<TourSearchRequestValidator>();
            container.RegisterType<
                IQueryHandler<TourSearchRequest, TourSearchResponse>,
                TourSearchHandler>();
            VerifyControllerRegistrations(container);
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void VerifyControllerRegistrations(IUnityContainer container)
        {
            var missingDependencies = typeof(UnityConfig).Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract && typeof(ApiController).IsAssignableFrom(type))
                .SelectMany(type => type.GetConstructors()
                    .SelectMany(constructor => constructor.GetParameters())
                    .Where(parameter => parameter.ParameterType.IsInterface || parameter.ParameterType.IsAbstract)
                    .Where(parameter => !container.IsRegistered(parameter.ParameterType))
                    .Select(parameter => $"{type.FullName}: {parameter.ParameterType.FullName}"))
                .Distinct()
                .ToArray();

            if (missingDependencies.Length > 0)
                throw new InvalidOperationException(
                    "Не зарегистрированы зависимости контроллеров:" + Environment.NewLine +
                    string.Join(Environment.NewLine, missingDependencies));
        }
    }
}
