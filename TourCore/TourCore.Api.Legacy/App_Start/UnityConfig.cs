#pragma warning disable 1591

using System.Collections.Generic;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Common.Models;
using TourCore.Application.Geography.Cities.Commands;
using TourCore.Application.Geography.Cities.Handlers;
using TourCore.Application.Geography.Cities.Queries;
using TourCore.Application.Geography.Cities.Validators;
using TourCore.Application.Geography.Countries.Commands;
using TourCore.Application.Geography.Countries.Handlers;
using TourCore.Application.Geography.Countries.Queries;
using TourCore.Application.Geography.Countries.Validators;
using TourCore.Application.Geography.Regions.Commands;
using TourCore.Application.Geography.Regions.Handlers;
using TourCore.Application.Geography.Regions.Queries;
using TourCore.Application.Geography.Regions.Validators;
using TourCore.Application.Geography.Resorts.Commands;
using TourCore.Application.Geography.Resorts.Handlers;
using TourCore.Application.Geography.Resorts.Queries;
using TourCore.Application.Geography.Resorts.Validators;
using TourCore.Application.Hotels.MealTypes.Commands;
using TourCore.Application.Hotels.MealTypes.Handlers;
using TourCore.Application.Hotels.MealTypes.Queries;
using TourCore.Application.Hotels.MealTypes.Validators;
using TourCore.Application.Hotels.RoomCategories.Commands;
using TourCore.Application.Hotels.RoomCategories.Handlers;
using TourCore.Application.Hotels.RoomCategories.Queries;
using TourCore.Application.Hotels.RoomCategories.Validators;
using TourCore.Contracts.Geography.Cities;
using TourCore.Contracts.Geography.Countries;
using TourCore.Contracts.Geography.Regions;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Infrastructure.Services;
using TourCore.Infrastructure.SqlServer.Persistence;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

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

            #region  Validators

            // Country Validators
            container.RegisterType<CreateCountryCommandValidator>();
            container.RegisterType<UpdateCountryCommandValidator>();

            // Region validators
            container.RegisterType<CreateRegionCommandValidator>();
            container.RegisterType<UpdateRegionCommandValidator>();

            // City validators
            container.RegisterType<CreateCityCommandValidator>();
            container.RegisterType<UpdateCityCommandValidator>();

            // Resort validators
            container.RegisterType<CreateResortCommandValidator>();
            container.RegisterType<UpdateResortCommandValidator>();

            // MealType validators
            container.RegisterType<CreateMealTypeCommandValidator>();
            container.RegisterType<UpdateMealTypeCommandValidator>();

            // RoomCategory validators
            container.RegisterType<CreateRoomCategoryCommandValidator>();
            container.RegisterType<UpdateRoomCategoryCommandValidator>();

            #endregion

            #region Commands

            // Country commands
            container.RegisterType<
                ICommandHandler<CreateCountryCommand, CountryDto>,
                CreateCountryHandler>();

            container.RegisterType<
                ICommandHandler<UpdateCountryCommand, CountryDto>,
                UpdateCountryHandler>();

            // Region commands
            container.RegisterType<
                ICommandHandler<CreateRegionCommand, RegionDto>,
                CreateRegionHandler>();

            container.RegisterType<
                ICommandHandler<UpdateRegionCommand, RegionDto>,
                UpdateRegionHandler>();

            // City commands
            container.RegisterType<
                ICommandHandler<CreateCityCommand, CityDto>,
                CreateCityHandler>();

            container.RegisterType<
                ICommandHandler<UpdateCityCommand, CityDto>,
                UpdateCityHandler>();

            // Resort commands
            container.RegisterType<
                ICommandHandler<CreateResortCommand, ResortDto>,
                CreateResortHandler>();

            container.RegisterType<
                ICommandHandler<UpdateResortCommand, ResortDto>,
                UpdateResortHandler>();

            // MealType commands
            container.RegisterType<
                ICommandHandler<CreateMealTypeCommand, MealTypeDto>,
                CreateMealTypeHandler>();

            container.RegisterType<
                ICommandHandler<UpdateMealTypeCommand, MealTypeDto>,
                UpdateMealTypeHandler>();

            // RoomCategory commands
            container.RegisterType<
                ICommandHandler<CreateRoomCategoryCommand, RoomCategoryDto>,
                CreateRoomCategoryHandler>();

            container.RegisterType<
                ICommandHandler<UpdateRoomCategoryCommand, RoomCategoryDto>,
                UpdateRoomCategoryHandler>();

            #endregion

            #region Queries

            // Country queries
            container.RegisterType<
                IQueryHandler<GetCountriesQuery, ListResult<CountryListItemDto>>,
                GetCountriesHandler>();

            container.RegisterType<
                IQueryHandler<GetCountryByIdQuery, CountryDto>,
                GetCountryByIdHandler>();

            // Region queries
            container.RegisterType<
                IQueryHandler<GetRegionsQuery, ListResult<RegionListItemDto>>,
                GetRegionsHandler>();

            container.RegisterType<
                IQueryHandler<GetRegionByIdQuery, RegionDto>,
                GetRegionByIdHandler>();

            // City queries
            container.RegisterType<
                IQueryHandler<GetCitiesQuery, ListResult<CityListItemDto>>,
                GetCitiesHandler>();

            container.RegisterType<
                IQueryHandler<GetCityByIdQuery, CityDto>,
                GetCityByIdHandler>();

            // Resort queries
            container.RegisterType<
                IQueryHandler<GetResortsQuery, ListResult<ResortListItemDto>>,
                GetResortsHandler>();

            container.RegisterType<
                IQueryHandler<GetResortByIdQuery, ResortDto>,
                GetResortByIdHandler>();

            // MealType queries
            container.RegisterType<
                IQueryHandler<GetMealTypesQuery, ListResult<MealTypeListItemDto>>,
                GetMealTypesHandler>();

            container.RegisterType<
                IQueryHandler<GetMealTypeByIdQuery, MealTypeDto>,
                GetMealTypeByIdHandler>();

            // RoomCategory queries
            container.RegisterType<
                IQueryHandler<GetRoomCategoriesQuery, ListResult<RoomCategoryListItemDto>>,
                GetRoomCategoriesHandler>();

            container.RegisterType<
                IQueryHandler<GetRoomCategoryByIdQuery, RoomCategoryDto>,
                GetRoomCategoryByIdHandler>();

            #endregion

            #region Persistence

            // Persistence
            container.RegisterType<TourCoreDbContext>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ICountryRepository, CountryRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IRegionRepository, RegionRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ICityRepository, CityRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IResortRepository, ResortRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IUnitOfWork, UnitOfWork>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IMealTypeRepository, MealTypeRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IRoomCategoryRepository, RoomCategoryRepository>(
                new HierarchicalLifetimeManager());

            #endregion
        }
    }
}

#pragma warning restore 1591