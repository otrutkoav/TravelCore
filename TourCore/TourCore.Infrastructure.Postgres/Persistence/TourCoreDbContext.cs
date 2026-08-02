using Microsoft.EntityFrameworkCore;
using TourCore.Domain.Avia.Entities;
using TourCore.Domain.Bus.Entities;
using TourCore.Domain.Finance.Entities;
using TourCore.Domain.Geography.Entities;
using TourCore.Domain.Hotels.Entities;
using TourCore.Domain.Railway.Entities;
using TourCore.Domain.Seating.Entities;
using TourCore.Domain.Transfers.Entities;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Infrastructure.Postgres.Persistence;

public sealed class TourCoreDbContext : DbContext
{
    public TourCoreDbContext(DbContextOptions<TourCoreDbContext> options) : base(options) { }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Region> Regions => Set<Region>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Resort> Resorts => Set<Resort>();
    public DbSet<Hotel> Hotels => Set<Hotel>();
    public DbSet<MealType> MealTypes => Set<MealType>();
    public DbSet<RoomCategory> RoomCategories => Set<RoomCategory>();
    public DbSet<RoomType> RoomTypes => Set<RoomType>();
    public DbSet<HotelCategory> HotelCategories => Set<HotelCategory>();
    public DbSet<AccommodationType> AccommodationTypes => Set<AccommodationType>();
    public DbSet<AccommodationPlacementRule> AccommodationPlacementRules => Set<AccommodationPlacementRule>();
    public DbSet<AccommodationPlacementRuleAgeRange> AccommodationPlacementRuleAgeRanges => Set<AccommodationPlacementRuleAgeRange>();
    public DbSet<Rate> Rates => Set<Rate>();
    public DbSet<RealCourse> RealCourses => Set<RealCourse>();
    public DbSet<AirClass> AirClasses => Set<AirClass>();
    public DbSet<Aircraft> Aircrafts => Set<Aircraft>();
    public DbSet<Airline> Airlines => Set<Airline>();
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Charter> Charters => Set<Charter>();
    public DbSet<CharterSeason> CharterSeasons => Set<CharterSeason>();
    public DbSet<BusTransfer> BusTransfers => Set<BusTransfer>();
    public DbSet<BusTransferPoint> BusTransferPoints => Set<BusTransferPoint>();
    public DbSet<BusSchedule> BusSchedules => Set<BusSchedule>();
    public DbSet<RailwayTransfer> RailwayTransfers => Set<RailwayTransfer>();
    public DbSet<TrainSchedule> TrainSchedules => Set<TrainSchedule>();
    public DbSet<Transport> Transports => Set<Transport>();
    public DbSet<Transfer> Transfers => Set<Transfer>();
    public DbSet<TransferDirection> TransferDirections => Set<TransferDirection>();
    public DbSet<SeatingCell> SeatingCells => Set<SeatingCell>();
    public DbSet<VehiclePlan> VehiclePlans => Set<VehiclePlan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TourCoreDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
