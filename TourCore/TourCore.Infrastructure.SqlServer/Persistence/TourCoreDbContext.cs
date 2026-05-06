using System.Data.Entity;
using TourCore.Domain.Avia.Entities;
using TourCore.Domain.Bus.Entities;
using TourCore.Domain.Finance.Entities;
using TourCore.Domain.Geography.Entities;
using TourCore.Domain.Hotels.Entities;
using TourCore.Domain.Railway.Entities;
using TourCore.Domain.Seating.Entities;
using TourCore.Domain.Transfers.Entities;
using TourCore.Domain.Transportation.Entities;
using TourCore.Infrastructure.Persistence.Configurations.Avia;
using TourCore.Infrastructure.SqlServer.Configurations;
using TourCore.Infrastructure.SqlServer.Configurations.Avia;
using TourCore.Infrastructure.SqlServer.Configurations.Bus;
using TourCore.Infrastructure.SqlServer.Configurations.Finance;
using TourCore.Infrastructure.SqlServer.Configurations.Hotels;
using TourCore.Infrastructure.SqlServer.Configurations.Railway;
using TourCore.Infrastructure.SqlServer.Configurations.Seating;
using TourCore.Infrastructure.SqlServer.Configurations.Transfers;
using TourCore.Infrastructure.SqlServer.Configurations.Transportation;

namespace TourCore.Infrastructure.SqlServer.Persistence
{
    public class TourCoreDbContext : DbContext
    {
        public TourCoreDbContext()
            : base("name=TourCoreDb")
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Resort> Resorts { get; set; }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }
        public DbSet<AccommodationType> AccommodationTypes { get; set; }
        public DbSet<AccommodationPlacementRule> AccommodationPlacementRules { get; set; }
        public DbSet<AccommodationPlacementRuleAgeRange> AccommodationPlacementRuleAgeRanges { get; set; }
       

        public DbSet<Rate> Rates { get; set; }
        public DbSet<RealCourse> RealCourses { get; set; }

        public DbSet<AirClass> AirClasses { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Charter> Charters { get; set; }
        public DbSet<CharterSeason> CharterSeasons { get; set; }

        public DbSet<BusTransfer> BusTransfers { get; set; }
        public DbSet<BusTransferPoint> BusTransferPoints { get; set; }
        public DbSet<BusSchedule> BusSchedules { get; set; }

        public DbSet<RailwayTransfer> RailwayTransfers { get; set; }
        public DbSet<TrainSchedule> TrainSchedules { get; set; }

        public DbSet<Transport> Transports { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferDirection> TransferDirections { get; set; }

        public DbSet<SeatingCell> SeatingCells { get; set; }
        public DbSet<VehiclePlan> VehiclePlans { get; set; }

      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ResortConfiguration());

            modelBuilder.Configurations.Add(new HotelConfiguration());
            modelBuilder.Configurations.Add(new MealTypeConfiguration());
            modelBuilder.Configurations.Add(new RoomCategoryConfiguration());
            modelBuilder.Configurations.Add(new RoomTypeConfiguration());
            modelBuilder.Configurations.Add(new HotelCategoryConfiguration());
            modelBuilder.Configurations.Add(new AccommodationTypeConfiguration());
            modelBuilder.Configurations.Add(new AccommodationPlacementRuleConfiguration());
            modelBuilder.Configurations.Add(new AccommodationPlacementRuleAgeRangeConfiguration());

            modelBuilder.Configurations.Add(new RateConfiguration());
            modelBuilder.Configurations.Add(new RealCourseConfiguration());

            modelBuilder.Configurations.Add(new AirClassConfiguration());
            modelBuilder.Configurations.Add(new AircraftConfiguration());
            modelBuilder.Configurations.Add(new AirlineConfiguration());
            modelBuilder.Configurations.Add(new AirportConfiguration());
            modelBuilder.Configurations.Add(new CharterConfiguration());
            modelBuilder.Configurations.Add(new CharterSeasonConfiguration());

            modelBuilder.Configurations.Add(new BusTransferConfiguration());
            modelBuilder.Configurations.Add(new BusTransferPointConfiguration());
            modelBuilder.Configurations.Add(new BusScheduleConfiguration());

            modelBuilder.Configurations.Add(new RailwayTransferConfiguration());
            modelBuilder.Configurations.Add(new TrainScheduleConfiguration());

            modelBuilder.Configurations.Add(new TransportConfiguration());

            modelBuilder.Configurations.Add(new TransferConfiguration());
            modelBuilder.Configurations.Add(new TransferDirectionConfiguration());

            modelBuilder.Configurations.Add(new SeatingCellConfiguration());
            modelBuilder.Configurations.Add(new VehiclePlanConfiguration());

            

            base.OnModelCreating(modelBuilder);
        }
    }
}