#pragma warning disable 1591

using System.Collections.Generic;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Abstractions.Persistence;
using TourCore.Application.Abstractions.Persistence.Avia;
using TourCore.Application.Abstractions.Persistence.Bus;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Abstractions.Persistence.Geography;
using TourCore.Application.Abstractions.Persistence.Hotels;
using TourCore.Application.Abstractions.Persistence.Railway;
using TourCore.Application.Abstractions.Persistence.Seating;
using TourCore.Application.Abstractions.Persistence.Transfers;
using TourCore.Application.Abstractions.Persistence.Transportation;
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
using TourCore.Application.Avia.Charters.Commands;
using TourCore.Application.Avia.Charters.Handlers;
using TourCore.Application.Avia.Charters.Queries;
using TourCore.Application.Avia.Charters.Validators;
using TourCore.Application.Avia.CharterSeasons.Commands;
using TourCore.Application.Avia.CharterSeasons.Handlers;
using TourCore.Application.Avia.CharterSeasons.Queries;
using TourCore.Application.Avia.CharterSeasons.Validators;
using TourCore.Application.Bus.BusSchedules.Commands;
using TourCore.Application.Bus.BusSchedules.Handlers;
using TourCore.Application.Bus.BusSchedules.Queries;
using TourCore.Application.Bus.BusSchedules.Validators;
using TourCore.Application.Bus.BusTransferPoints.Commands;
using TourCore.Application.Bus.BusTransferPoints.Handlers;
using TourCore.Application.Bus.BusTransferPoints.Queries;
using TourCore.Application.Bus.BusTransferPoints.Validators;
using TourCore.Application.Bus.BusTransfers.Commands;
using TourCore.Application.Bus.BusTransfers.Handlers;
using TourCore.Application.Bus.BusTransfers.Queries;
using TourCore.Application.Bus.BusTransfers.Validators;
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
using TourCore.Application.Hotels.AccommodationPlacementRules.Commands;
using TourCore.Application.Hotels.AccommodationPlacementRules.Handlers;
using TourCore.Application.Hotels.AccommodationPlacementRules.Queries;
using TourCore.Application.Hotels.AccommodationPlacementRules.Validators;
using TourCore.Application.Hotels.AccommodationTypes.Commands;
using TourCore.Application.Hotels.AccommodationTypes.Handlers;
using TourCore.Application.Hotels.AccommodationTypes.Queries;
using TourCore.Application.Hotels.AccommodationTypes.Validators;
using TourCore.Application.Hotels.HotelCategories.Commands;
using TourCore.Application.Hotels.HotelCategories.Handlers;
using TourCore.Application.Hotels.HotelCategories.Queries;
using TourCore.Application.Hotels.HotelCategories.Validators;
using TourCore.Application.Hotels.Hotels.Commands;
using TourCore.Application.Hotels.Hotels.Handlers;
using TourCore.Application.Hotels.Hotels.Queries;
using TourCore.Application.Hotels.Hotels.Validators;
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
using TourCore.Application.Railway.RailwayTransfers.Commands;
using TourCore.Application.Railway.RailwayTransfers.Handlers;
using TourCore.Application.Railway.RailwayTransfers.Queries;
using TourCore.Application.Railway.RailwayTransfers.Validators;
using TourCore.Application.Railway.TrainSchedules.Commands;
using TourCore.Application.Railway.TrainSchedules.Handlers;
using TourCore.Application.Railway.TrainSchedules.Queries;
using TourCore.Application.Railway.TrainSchedules.Validators;
using TourCore.Application.Seating.SeatingCells.Commands;
using TourCore.Application.Seating.SeatingCells.Handlers;
using TourCore.Application.Seating.SeatingCells.Queries;
using TourCore.Application.Seating.SeatingCells.Validators;
using TourCore.Application.Seating.VehiclePlans.Commands;
using TourCore.Application.Seating.VehiclePlans.Handlers;
using TourCore.Application.Seating.VehiclePlans.Queries;
using TourCore.Application.Seating.VehiclePlans.Validators;
using TourCore.Application.Transfers.TransferDirections.Commands;
using TourCore.Application.Transfers.TransferDirections.Handlers;
using TourCore.Application.Transfers.TransferDirections.Queries;
using TourCore.Application.Transfers.TransferDirections.Validators;
using TourCore.Application.Transfers.Transfers.Commands;
using TourCore.Application.Transfers.Transfers.Handlers;
using TourCore.Application.Transfers.Transfers.Queries;
using TourCore.Application.Transfers.Transfers.Validators;
using TourCore.Application.Transportation.Transports.Commands;
using TourCore.Application.Transportation.Transports.Handlers;
using TourCore.Application.Transportation.Transports.Queries;
using TourCore.Application.Transportation.Transports.Validators;
using TourCore.Contracts.Avia.AirClasses;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Contracts.Avia.Airlines;
using TourCore.Contracts.Avia.Airports;
using TourCore.Contracts.Avia.Charters;
using TourCore.Contracts.Avia.CharterSeasons;
using TourCore.Contracts.Bus.BusSchedules;
using TourCore.Contracts.Bus.BusTransferPoints;
using TourCore.Contracts.Bus.BusTransfers;
using TourCore.Contracts.Common;
using TourCore.Contracts.Finance.Rates;
using TourCore.Contracts.Finance.RealCourses;
using TourCore.Contracts.Geography.Cities;
using TourCore.Contracts.Geography.Countries;
using TourCore.Contracts.Geography.Regions;
using TourCore.Contracts.Geography.Resorts;
using TourCore.Contracts.Hotels.AccommodationPlacementRules;
using TourCore.Contracts.Hotels.AccommodationTypes;
using TourCore.Contracts.Hotels.HotelCategories;
using TourCore.Contracts.Hotels.Hotels;
using TourCore.Contracts.Hotels.MealTypes;
using TourCore.Contracts.Hotels.RoomCategories;
using TourCore.Contracts.Hotels.RoomTypes;
using TourCore.Contracts.Railway.RailwayTransfers;
using TourCore.Contracts.Railway.TrainSchedules;
using TourCore.Contracts.Seating.SeatingCells;
using TourCore.Contracts.Seating.VehiclePlans;
using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Contracts.Transfers.Transfers;
using TourCore.Contracts.Transportation.Transports;
using TourCore.Infrastructure.Services;
using TourCore.Infrastructure.SqlServer.Persistence;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Avia;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Bus;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Finance;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Hotels;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Railway;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Seating;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Transfers;
using TourCore.Infrastructure.SqlServer.Persistence.Repositories.Transportation;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using TourCore.Application.Common.Data;
using TourCore.Application.Common.Queries;
using TourCore.Infrastructure.SqlServer.Common;

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

            container.RegisterType<IAsyncQueryExecutor, Ef6AsyncQueryExecutor>();

            container.RegisterType<PagedQueryExecutor>();

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

            // Charter validators
            container.RegisterType<CreateCharterCommandValidator>();
            container.RegisterType<UpdateCharterCommandValidator>();

            // CharterSeason validators
            container.RegisterType<CreateCharterSeasonCommandValidator>();
            container.RegisterType<UpdateCharterSeasonCommandValidator>();

            // BusTransfer validators
            container.RegisterType<CreateBusTransferCommandValidator>();
            container.RegisterType<UpdateBusTransferCommandValidator>();

            // BusTransferPoint validators
            container.RegisterType<CreateBusTransferPointCommandValidator>();
            container.RegisterType<UpdateBusTransferPointCommandValidator>();

            // BusSchedule validators
            container.RegisterType<CreateBusScheduleCommandValidator>();
            container.RegisterType<UpdateBusScheduleCommandValidator>();

            // RailwayTransfer validators
            container.RegisterType<CreateRailwayTransferCommandValidator>();
            container.RegisterType<UpdateRailwayTransferCommandValidator>();

            // TrainSchedule validators
            container.RegisterType<CreateTrainScheduleCommandValidator>();
            container.RegisterType<UpdateTrainScheduleCommandValidator>();

            // Transport validators
            container.RegisterType<CreateTransportCommandValidator>();
            container.RegisterType<UpdateTransportCommandValidator>();

            // Transfer validators
            container.RegisterType<CreateTransferCommandValidator>();
            container.RegisterType<UpdateTransferCommandValidator>();

            // TransferDirection validators
            container.RegisterType<CreateTransferDirectionCommandValidator>();
            container.RegisterType<UpdateTransferDirectionCommandValidator>();

            // SeatingCell validators
            container.RegisterType<CreateSeatingCellCommandValidator>();
            container.RegisterType<UpdateSeatingCellCommandValidator>();

            // VehiclePlan validators
            container.RegisterType<CreateVehiclePlanCommandValidator>();
            container.RegisterType<UpdateVehiclePlanCommandValidator>();

            // AccommodationPlacementRule validators
            container.RegisterType<CreateAccommodationPlacementRuleCommandValidator>();
            container.RegisterType<UpdateAccommodationPlacementRuleCommandValidator>();

            // AccommodationType validators
            container.RegisterType<CreateAccommodationTypeCommandValidator>();
            container.RegisterType<UpdateAccommodationTypeCommandValidator>();

            // Hotel validators
            container.RegisterType<CreateHotelCommandValidator>();
            container.RegisterType<UpdateHotelCommandValidator>();

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

            // Hotel commands
            container.RegisterType<
                ICommandHandler<CreateHotelCommand, HotelDto>,
                CreateHotelHandler>();

            container.RegisterType<
                ICommandHandler<UpdateHotelCommand, HotelDto>,
                UpdateHotelHandler>();

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

            // Charter commands
            container.RegisterType<
                ICommandHandler<CreateCharterCommand, CharterDto>,
                CreateCharterHandler>();

            container.RegisterType<
                ICommandHandler<UpdateCharterCommand, CharterDto>,
                UpdateCharterHandler>();

            // CharterSeason commands
            container.RegisterType<
                ICommandHandler<CreateCharterSeasonCommand, CharterSeasonDto>,
                CreateCharterSeasonHandler>();

            container.RegisterType<
                ICommandHandler<UpdateCharterSeasonCommand, CharterSeasonDto>,
                UpdateCharterSeasonHandler>();

            // BusTransfer commands
            container.RegisterType<
                ICommandHandler<CreateBusTransferCommand, BusTransferDto>,
                CreateBusTransferHandler>();

            container.RegisterType<
                ICommandHandler<UpdateBusTransferCommand, BusTransferDto>,
                UpdateBusTransferHandler>();

            // BusTransferPoint commands
            container.RegisterType<
                ICommandHandler<CreateBusTransferPointCommand, BusTransferPointDto>,
                CreateBusTransferPointHandler>();

            container.RegisterType<
                ICommandHandler<UpdateBusTransferPointCommand, BusTransferPointDto>,
                UpdateBusTransferPointHandler>();

            // BusSchedule commands
            container.RegisterType<
                ICommandHandler<CreateBusScheduleCommand, BusScheduleDto>,
                CreateBusScheduleHandler>();

            container.RegisterType<
                ICommandHandler<UpdateBusScheduleCommand, BusScheduleDto>,
                UpdateBusScheduleHandler>();

            // RailwayTransfer commands
            container.RegisterType<
                ICommandHandler<CreateRailwayTransferCommand, RailwayTransferDto>,
                CreateRailwayTransferHandler>();

            container.RegisterType<
                ICommandHandler<UpdateRailwayTransferCommand, RailwayTransferDto>,
                UpdateRailwayTransferHandler>();

            // TrainSchedule commands
            container.RegisterType<
                ICommandHandler<CreateTrainScheduleCommand, TrainScheduleDto>,
                CreateTrainScheduleHandler>();

            container.RegisterType<
                ICommandHandler<UpdateTrainScheduleCommand, TrainScheduleDto>,
                UpdateTrainScheduleHandler>();

            // Transport commands
            container.RegisterType<
                ICommandHandler<CreateTransportCommand, TransportDto>,
                CreateTransportHandler>();

            container.RegisterType<
                ICommandHandler<UpdateTransportCommand, TransportDto>,
                UpdateTransportHandler>();

            // Transfer commands
            container.RegisterType<
                ICommandHandler<CreateTransferCommand, TransferDto>,
                CreateTransferHandler>();

            container.RegisterType<
                ICommandHandler<UpdateTransferCommand, TransferDto>,
                UpdateTransferHandler>();

            // TransferDirection commands
            container.RegisterType<
                ICommandHandler<CreateTransferDirectionCommand, TransferDirectionDto>,
                CreateTransferDirectionHandler>();

            container.RegisterType<
                ICommandHandler<UpdateTransferDirectionCommand, TransferDirectionDto>,
                UpdateTransferDirectionHandler>();

            // SeatingCell commands
            container.RegisterType<
                ICommandHandler<CreateSeatingCellCommand, SeatingCellDto>,
                CreateSeatingCellHandler>();

            container.RegisterType<
                ICommandHandler<UpdateSeatingCellCommand, SeatingCellDto>,
                UpdateSeatingCellHandler>();

            // VehiclePlan commands
            container.RegisterType<
                ICommandHandler<CreateVehiclePlanCommand, VehiclePlanDto>,
                CreateVehiclePlanHandler>();

            container.RegisterType<
                ICommandHandler<UpdateVehiclePlanCommand, VehiclePlanDto>,
                UpdateVehiclePlanHandler>();

            // AccommodationPlacementRule commands
            container.RegisterType<
                ICommandHandler<CreateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto>,
                CreateAccommodationPlacementRuleHandler>();

            container.RegisterType<
                ICommandHandler<UpdateAccommodationPlacementRuleCommand, AccommodationPlacementRuleDto>,
                UpdateAccommodationPlacementRuleHandler>();

            // AccommodationType commands
            container.RegisterType<
                ICommandHandler<CreateAccommodationTypeCommand, AccommodationTypeDto>,
                CreateAccommodationTypeHandler>();

            container.RegisterType<
                ICommandHandler<UpdateAccommodationTypeCommand, AccommodationTypeDto>,
                UpdateAccommodationTypeHandler>();

            #endregion

            #region Queries

            // Country queries
            container.RegisterType<
                IQueryHandler<GetCountriesQuery, PagedResponseDto<CountryListItemDto>>,
                GetCountriesHandler>();

            container.RegisterType<
                IQueryHandler<GetCountryByIdQuery, CountryDto>,
                GetCountryByIdHandler>();

            // Region queries
            container.RegisterType<
                IQueryHandler<GetRegionsQuery, PagedResponseDto<RegionListItemDto>>,
                GetRegionsHandler>();

            container.RegisterType<
                IQueryHandler<GetRegionByIdQuery, RegionDto>,
                GetRegionByIdHandler>();

            // City queries
            container.RegisterType<
                IQueryHandler<GetCitiesQuery, PagedResponseDto<CityListItemDto>>,
                GetCitiesHandler>();

            container.RegisterType<
                IQueryHandler<GetCityByIdQuery, CityDto>,
                GetCityByIdHandler>();

            // Resort queries
            container.RegisterType<
                IQueryHandler<GetResortsQuery, PagedResponseDto<ResortListItemDto>>,
                GetResortsHandler>();

            container.RegisterType<
                IQueryHandler<GetResortByIdQuery, ResortDto>,
                GetResortByIdHandler>();

            // MealType queries
            container.RegisterType<
                IQueryHandler<GetMealTypesQuery, PagedResponseDto<MealTypeListItemDto>>,
                GetMealTypesHandler>();

            container.RegisterType<
                IQueryHandler<GetMealTypeByIdQuery, MealTypeDto>,
                GetMealTypeByIdHandler>();

            // RoomCategory queries
            container.RegisterType<
                IQueryHandler<GetRoomCategoriesQuery, PagedResponseDto<RoomCategoryListItemDto>>,
                GetRoomCategoriesHandler>();

            container.RegisterType<
                IQueryHandler<GetRoomCategoryByIdQuery, RoomCategoryDto>,
                GetRoomCategoryByIdHandler>();

            // RoomType queries
            container.RegisterType<
                IQueryHandler<GetRoomTypesQuery, PagedResponseDto<RoomTypeListItemDto>>,
                GetRoomTypesHandler>();

            container.RegisterType<
                IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto>,
                GetRoomTypeByIdHandler>();

            // HotelCategory queries
            container.RegisterType<
                IQueryHandler<GetHotelCategoriesQuery, PagedResponseDto<HotelCategoryListItemDto>>,
                GetHotelCategoriesHandler>();

            container.RegisterType<
                IQueryHandler<GetHotelCategoryByIdQuery, HotelCategoryDto>,
                GetHotelCategoryByIdHandler>();

            // Rate queries
            container.RegisterType<
                IQueryHandler<GetRatesQuery, PagedResponseDto<RateListItemDto>>,
                GetRatesHandler>();

            container.RegisterType<
                IQueryHandler<GetRateByIdQuery, RateDto>,
                GetRateByIdHandler>();

            // RealCourse queries
            container.RegisterType<
                IQueryHandler<GetRealCoursesQuery, PagedResponseDto<RealCourseListItemDto>>,
                GetRealCoursesHandler>();

            container.RegisterType<
                IQueryHandler<GetRealCourseByIdQuery, RealCourseDto>,
                GetRealCourseByIdHandler>();

            // AirClass queries
            container.RegisterType<
                IQueryHandler<GetAirClassesQuery, PagedResponseDto<AirClassListItemDto>>,
                GetAirClassesHandler>();

            container.RegisterType<
                IQueryHandler<GetAirClassByIdQuery, AirClassDto>,
                GetAirClassByIdHandler>();

            // Aircraft queries
            container.RegisterType<
                IQueryHandler<GetAircraftsQuery, PagedResponseDto<AircraftListItemDto>>,
                GetAircraftsHandler>();

            container.RegisterType<
                IQueryHandler<GetAircraftByIdQuery, AircraftDto>,
                GetAircraftByIdHandler>();

            // Airline queries
            container.RegisterType<
                IQueryHandler<GetAirlinesQuery, PagedResponseDto<AirlineListItemDto>>,
                GetAirlinesHandler>();

            container.RegisterType<
                IQueryHandler<GetAirlineByIdQuery, AirlineDto>,
                GetAirlineByIdHandler>();

            // Airport queries
            container.RegisterType<
                IQueryHandler<GetAirportsQuery, PagedResponseDto<AirportListItemDto>>,
                GetAirportsHandler>();

            container.RegisterType<
                IQueryHandler<GetAirportByIdQuery, AirportDto>,
                GetAirportByIdHandler>();

            // Charter queries
            container.RegisterType<
                IQueryHandler<GetChartersQuery, PagedResponseDto<CharterListItemDto>>,
                GetChartersHandler>();

            container.RegisterType<
                IQueryHandler<GetCharterByIdQuery, CharterDto>,
                GetCharterByIdHandler>();

            // CharterSeason queries
            container.RegisterType<
                IQueryHandler<GetCharterSeasonByIdQuery, CharterSeasonDto>,
                GetCharterSeasonByIdHandler>();

            container.RegisterType<
                IQueryHandler<GetCharterSeasonsQuery, PagedResponseDto<CharterSeasonListItemDto>>,
                GetCharterSeasonsHandler>();

            // BusTransfer queries
            container.RegisterType<
                IQueryHandler<GetBusTransfersQuery, PagedResponseDto<BusTransferListItemDto>>,
                GetBusTransfersHandler>();

            container.RegisterType<
                IQueryHandler<GetBusTransferByIdQuery, BusTransferDto>,
                GetBusTransferByIdHandler>();

            // BusTransferPoint queries
            container.RegisterType<
                IQueryHandler<GetBusTransferPointsQuery, PagedResponseDto<BusTransferPointListItemDto>>,
                GetBusTransferPointsHandler>();

            container.RegisterType<
                IQueryHandler<GetBusTransferPointByIdQuery, BusTransferPointDto>,
                GetBusTransferPointByIdHandler>();

            // BusSchedule queries
            container.RegisterType<
                IQueryHandler<GetBusSchedulesQuery, PagedResponseDto<BusScheduleListItemDto>>,
                GetBusSchedulesHandler>();

            container.RegisterType<
                IQueryHandler<GetBusScheduleByIdQuery, BusScheduleDto>,
                GetBusScheduleByIdHandler>();

            // RailwayTransfer queries
            container.RegisterType<
                IQueryHandler<GetRailwayTransfersQuery, PagedResponseDto<RailwayTransferListItemDto>>,
                GetRailwayTransfersHandler>();

            container.RegisterType<
                IQueryHandler<GetRailwayTransferByIdQuery, RailwayTransferDto>,
                GetRailwayTransferByIdHandler>();

            // TrainSchedule queries
            container.RegisterType<
                IQueryHandler<GetTrainSchedulesQuery, PagedResponseDto<TrainScheduleListItemDto>>,
                GetTrainSchedulesHandler>();

            container.RegisterType<
                IQueryHandler<GetTrainScheduleByIdQuery, TrainScheduleDto>,
                GetTrainScheduleByIdHandler>();

            // Transport queries
            container.RegisterType<
                IQueryHandler<GetTransportsQuery, PagedResponseDto<TransportListItemDto>>,
                GetTransportsHandler>();

            container.RegisterType<
                IQueryHandler<GetTransportByIdQuery, TransportDto>,
                GetTransportByIdHandler>();

            // Transfer queries
            container.RegisterType<
                IQueryHandler<GetTransfersQuery, PagedResponseDto<TransferListItemDto>>,
                GetTransfersHandler>();

            container.RegisterType<
                IQueryHandler<GetTransferByIdQuery, TransferDto>,
                GetTransferByIdHandler>();

            // TransferDirection queries
            container.RegisterType<
                IQueryHandler<GetTransferDirectionsQuery, PagedResponseDto<TransferDirectionListItemDto>>,
                GetTransferDirectionsHandler>();

            container.RegisterType<
                IQueryHandler<GetTransferDirectionByIdQuery, TransferDirectionDto>,
                GetTransferDirectionByIdHandler>();

            // SeatingCell queries
            container.RegisterType<
                IQueryHandler<GetSeatingCellsQuery, PagedResponseDto<SeatingCellListItemDto>>,
                GetSeatingCellsHandler>();

            container.RegisterType<
                IQueryHandler<GetSeatingCellByIdQuery, SeatingCellDto>,
                GetSeatingCellByIdHandler>();

            // VehiclePlan queries
            container.RegisterType<
                IQueryHandler<GetVehiclePlansQuery, PagedResponseDto<VehiclePlanListItemDto>>,
                GetVehiclePlansHandler>();

            container.RegisterType<
                IQueryHandler<GetVehiclePlanByIdQuery, VehiclePlanDto>,
                GetVehiclePlanByIdHandler>();

            // AccommodationPlacementRule queries
            container.RegisterType<
                IQueryHandler<GetAccommodationPlacementRulesQuery, PagedResponseDto<AccommodationPlacementRuleListItemDto>>,
                GetAccommodationPlacementRulesHandler>();

            container.RegisterType<
                IQueryHandler<GetAccommodationPlacementRuleByIdQuery, AccommodationPlacementRuleDto>,
                GetAccommodationPlacementRuleByIdHandler>();

            // AccommodationType queries
            container.RegisterType<
                IQueryHandler<GetAccommodationTypesQuery, PagedResponseDto<AccommodationTypeListItemDto>>,
                GetAccommodationTypesHandler>();

            container.RegisterType<
                IQueryHandler<GetAccommodationTypeByIdQuery, AccommodationTypeDto>,
                GetAccommodationTypeByIdHandler>();

            // Hotel queries
            container.RegisterType<
                IQueryHandler<GetHotelsQuery, PagedResponseDto<HotelListItemDto>>,
                GetHotelsHandler>();

            container.RegisterType<
                IQueryHandler<GetHotelByIdQuery, HotelDto>,
                GetHotelByIdHandler>();

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

            container.RegisterType<ICharterRepository, CharterRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ICharterSeasonRepository, CharterSeasonRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IBusTransferRepository, BusTransferRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IBusTransferPointRepository, BusTransferPointRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IBusScheduleRepository, BusScheduleRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IRailwayTransferRepository, RailwayTransferRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ITrainScheduleRepository, TrainScheduleRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ITransportRepository, TransportRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ITransferRepository, TransferRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ITransferDirectionRepository, TransferDirectionRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<ISeatingCellRepository, SeatingCellRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IVehiclePlanRepository, VehiclePlanRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IAccommodationPlacementRuleRepository, AccommodationPlacementRuleRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IAccommodationTypeRepository, AccommodationTypeRepository>(
                new HierarchicalLifetimeManager());

            container.RegisterType<IHotelRepository, HotelRepository>(
                new HierarchicalLifetimeManager());

            #endregion
        }
    }
}

#pragma warning restore 1591