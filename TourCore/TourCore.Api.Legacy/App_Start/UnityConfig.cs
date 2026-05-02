#pragma warning disable 1591

using System.Collections.Generic;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Services;
using TourCore.Application.Avia.AirClasses.Commands;
using TourCore.Application.Avia.AirClasses.Handlers;
using TourCore.Application.Avia.AirClasses.Queries;
using TourCore.Application.Avia.AirClasses.Validators;
using TourCore.Application.Avia.Aircrafts.Commands;
using TourCore.Application.Avia.Aircrafts.Handlers;
using TourCore.Application.Avia.Aircrafts.Queries;
using TourCore.Application.Avia.Aircrafts.Validators;
using TourCore.Application.Avia.Airlines.Commands;
using TourCore.Application.Avia.Airlines.Handlers;
using TourCore.Application.Avia.Airlines.Queries;
using TourCore.Application.Avia.Airlines.Validators;
using TourCore.Application.Avia.Airports.Commands;
using TourCore.Application.Avia.Airports.Handlers;
using TourCore.Application.Avia.Airports.Queries;
using TourCore.Application.Avia.Airports.Validators;
using TourCore.Application.Common.Models;
using TourCore.Application.Finance.Rates.Commands;
using TourCore.Application.Finance.Rates.Handlers;
using TourCore.Application.Finance.Rates.Queries;
using TourCore.Application.Finance.Rates.Validators;
using TourCore.Application.Finance.RealCourses.Commands;
using TourCore.Application.Finance.RealCourses.Handlers;
using TourCore.Application.Finance.RealCourses.Queries;
using TourCore.Application.Finance.RealCourses.Validators;
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
using TourCore.Application.Hotels.HotelCategories.Commands;
using TourCore.Application.Hotels.HotelCategories.Handlers;
using TourCore.Application.Hotels.HotelCategories.Queries;
using TourCore.Application.Hotels.HotelCategories.Validators;
using TourCore.Application.Hotels.MealTypes.Commands;
using TourCore.Application.Hotels.MealTypes.Handlers;
using TourCore.Application.Hotels.MealTypes.Queries;
using TourCore.Application.Hotels.MealTypes.Validators;
using TourCore.Application.Hotels.RoomCategories.Commands;
using TourCore.Application.Hotels.RoomCategories.Handlers;
using TourCore.Application.Hotels.RoomCategories.Queries;
using TourCore.Application.Hotels.RoomCategories.Validators;
using TourCore.Application.Hotels.RoomTypes.Commands;
using TourCore.Application.Hotels.RoomTypes.Handlers;
using TourCore.Application.Hotels.RoomTypes.Queries;
using TourCore.Application.Hotels.RoomTypes.Validators;
using TourCore.Contracts.Avia.AirClasses;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Contracts.Avia.Airports;
using TourCore.Contracts.Finance.Rates;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Contracts.Geography.Cities;
using TourCore.Contracts.Geography.Countries;
using TourCore.Contracts.Geography.Regions;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Contracts.Hotels.RoomTypes;
using TourCore.Infrastructure.Services;
using TourCore.Infrastructure.SqlServer.Persistence;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Finance;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels;
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

            // RoomType validators
            container.RegisterType<CreateRoomTypeCommandValidator>();
            container.RegisterType<UpdateRoomTypeCommandValidator>();

            // HotelCategory validators
            container.RegisterType<CreateHotelCategoryCommandValidator>();
            container.RegisterType<UpdateHotelCategoryCommandValidator>();

            // Rate validators
            container.RegisterType<CreateRateCommandValidator>();
            container.RegisterType<UpdateRateCommandValidator>();

            // RealCourse validators
            container.RegisterType<CreateRealCourseCommandValidator>();
            container.RegisterType<UpdateRealCourseCommandValidator>();

            // AirClass validators
            container.RegisterType<CreateAirClassCommandValidator>();
            container.RegisterType<UpdateAirClassCommandValidator>();

            // Aircraft validators
            container.RegisterType<CreateAircraftCommandValidator>();
            container.RegisterType<UpdateAircraftCommandValidator>();

            // Airline validators
            container.RegisterType<CreateAirlineCommandValidator>();
            container.RegisterType<UpdateAirlineCommandValidator>();

            // Airport validators
            container.RegisterType<CreateAirportCommandValidator>();
            container.RegisterType<UpdateAirportCommandValidator>();

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

            // RoomType commands
            container.RegisterType<
                ICommandHandler<CreateRoomTypeCommand, RoomTypeDto>,
                CreateRoomTypeHandler>();

            container.RegisterType<
                ICommandHandler<UpdateRoomTypeCommand, RoomTypeDto>,
                UpdateRoomTypeHandler>();

            // HotelCategory commands
            container.RegisterType<
                ICommandHandler<CreateHotelCategoryCommand, HotelCategoryDto>,
                CreateHotelCategoryHandler>();

            container.RegisterType<
                ICommandHandler<UpdateHotelCategoryCommand, HotelCategoryDto>,
                UpdateHotelCategoryHandler>();

            // Rate commands
            container.RegisterType<
                ICommandHandler<CreateRateCommand, RateDto>,
                CreateRateHandler>();

            container.RegisterType<
                ICommandHandler<UpdateRateCommand, RateDto>,
                UpdateRateHandler>();

            // RealCourse commands
            container.RegisterType<
                ICommandHandler<CreateRealCourseCommand, RealCourseDto>,
                CreateRealCourseHandler>();

            container.RegisterType<
                ICommandHandler<UpdateRealCourseCommand, RealCourseDto>,
                UpdateRealCourseHandler>();

            // AirClass commands
            container.RegisterType<
                ICommandHandler<CreateAirClassCommand, AirClassDto>,
                CreateAirClassHandler>();

            container.RegisterType<
                ICommandHandler<UpdateAirClassCommand, AirClassDto>,
                UpdateAirClassHandler>();

            // Aircraft commands
            container.RegisterType<
                ICommandHandler<CreateAircraftCommand, AircraftDto>,
                CreateAircraftHandler>();

            container.RegisterType<
                ICommandHandler<UpdateAircraftCommand, AircraftDto>,
                UpdateAircraftHandler>();

            // Airline commands
            container.RegisterType<
                ICommandHandler<CreateAirlineCommand, AirlineDto>,
                CreateAirlineHandler>();

            container.RegisterType<
                ICommandHandler<UpdateAirlineCommand, AirlineDto>,
                UpdateAirlineHandler>();

            // Airport commands
            container.RegisterType<
                ICommandHandler<CreateAirportCommand, AirportDto>,
                CreateAirportHandler>();

            container.RegisterType<
                ICommandHandler<UpdateAirportCommand, AirportDto>,
                UpdateAirportHandler>();

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

            // RoomType queries
            container.RegisterType<
                IQueryHandler<GetRoomTypesQuery, ListResult<RoomTypeListItemDto>>,
                GetRoomTypesHandler>();

            container.RegisterType<
                IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>,
                GetRoomTypeByIdHandler>();

            // HotelCategory queries
            container.RegisterType<
                IQueryHandler<GetHotelCategoriesQuery, ListResult<HotelCategoryListItemDto>>,
                GetHotelCategoriesHandler>();

            container.RegisterType<
                IQueryHandler<GetHotelCategoryByIdQuery, HotelCategoryDto>,
                GetHotelCategoryByIdHandler>();

            // Rate queries
            container.RegisterType<
                IQueryHandler<GetRatesQuery, ListResult<RateListItemDto>>,
                GetRatesHandler>();

            container.RegisterType<
                IQueryHandler<GetRateByIdQuery, RateDto>,
                GetRateByIdHandler>();

            // RealCourse queries
            container.RegisterType<
                IQueryHandler<GetRealCoursesQuery, ListResult<RealCourseListItemDto>>,
                GetRealCoursesHandler>();

            container.RegisterType<
                IQueryHandler<GetRealCourseByIdQuery, RealCourseDto>,
                GetRealCourseByIdHandler>();

            // AirClass queries
            container.RegisterType<
                IQueryHandler<GetAirClassesQuery, ListResult<AirClassListItemDto>>,
                GetAirClassesHandler>();

            container.RegisterType<
                IQueryHandler<GetAirClassByIdQuery, AirClassDto>,
                GetAirClassByIdHandler>();

            // Aircraft queries
            container.RegisterType<
                IQueryHandler<GetAircraftsQuery, ListResult<AircraftListItemDto>>,
                GetAircraftsHandler>();

            container.RegisterType<
                IQueryHandler<GetAircraftByIdQuery, AircraftDto>,
                GetAircraftByIdHandler>();

            // Airline queries
            container.RegisterType<
                IQueryHandler<GetAirlinesQuery, ListResult<AirlineListItemDto>>,
                GetAirlinesHandler>();

            container.RegisterType<
                IQueryHandler<GetAirlineByIdQuery, AirlineDto>,
                GetAirlineByIdHandler>();

            // Airport queries
            container.RegisterType<
                IQueryHandler<GetAirportsQuery, ListResult<AirportListItemDto>>,
                GetAirportsHandler>();

            container.RegisterType<
                IQueryHandler<GetAirportByIdQuery, AirportDto>,
                GetAirportByIdHandler>();

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

            container.RegisterType<IRoomTypeRepository, RoomTypeRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IHotelCategoryRepository, HotelCategoryRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IRateRepository, RateRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IRealCourseRepository, RealCourseRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IAirClassRepository, AirClassRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IAircraftRepository, AircraftRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IAirlineRepository, AirlineRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IAirportRepository, AirportRepository>(
                new HierarchicalLifetimeManager());

            #endregion
        }
    }
}

#pragma warning restore 1591