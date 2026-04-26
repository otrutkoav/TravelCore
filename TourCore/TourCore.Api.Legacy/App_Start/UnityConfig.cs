using System.Collections.Generic;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Countries.Commands;
using TourCore.Application.Countries.Handlers;
using TourCore.Application.Countries.Queries;
using TourCore.Application.Countries.Validators;
using TourCore.Contracts.Geography.Countries;
using Unity;
using Unity.WebApi;
using TourCore.Infrastructure.Services;
using TourCore.Application.Common.Models;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Infrastructure.SqlServer.Persistence;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories;
using Unity.Lifetime;

namespace TourCore.Api.Legacy
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            RegisterApplication(container);

            GlobalConfiguration.Configuration.DependencyResolver =
                new UnityDependencyResolver(container);
        }

        private static void RegisterApplication(IUnityContainer container)
        {
            // Services
            container.RegisterType<IDateTimeProvider, DateTimeProvider>();

            // Validators
            container.RegisterType<CreateCountryCommandValidator>();
            container.RegisterType<UpdateCountryCommandValidator>();

            // Country commands
            container.RegisterType<
                ICommandHandler<CreateCountryCommand, CountryDto>,
                CreateCountryHandler>();

            container.RegisterType<
                ICommandHandler<UpdateCountryCommand, CountryDto>,
                UpdateCountryHandler>();

            // Country queries
            container.RegisterType<
                IQueryHandler<GetCountriesQuery, ListResult<CountryListItemDto>>,
                GetCountriesHandler>();

            container.RegisterType<
                IQueryHandler<GetCountryByIdQuery, CountryDto>,
                GetCountryByIdHandler>();

            // Persistence
            // Persistence
            container.RegisterType<TourCoreDbContext>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ICountryRepository, CountryRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IUnitOfWork, UnitOfWork>(
                new HierarchicalLifetimeManager());
        }
    }
}