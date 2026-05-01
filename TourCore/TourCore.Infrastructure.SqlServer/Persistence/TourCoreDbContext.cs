using System.Data.Entity;
using TourCore.Domain.Avia.Entities;
using TourCore.Domain.Finance.Entities;
using TourCore.Domain.Geography.Entities;
using TourCore.Domain.Hotels.Entities;
using TourCore.Infrastructure.Persistence.Configurations.Avia;
using TourCore.Infrastructure.SqlServer.Configurations;
using TourCore.Infrastructure.SqlServer.Configurations.Avia;
using TourCore.Infrastructure.SqlServer.Configurations.Finance;
using TourCore.Infrastructure.SqlServer.Configurations.Hotels;

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

        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelCategory> HotelCategories { get; set; }

        public DbSet<Rate> Rates { get; set; }
        public DbSet<RealCourse> RealCourses { get; set; }

        public DbSet<AirClass> AirClasses { get; set; }
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airline> Airlines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ResortConfiguration());

            modelBuilder.Configurations.Add(new MealTypeConfiguration());
            modelBuilder.Configurations.Add(new RoomCategoryConfiguration());
            modelBuilder.Configurations.Add(new RoomTypeConfiguration());
            modelBuilder.Configurations.Add(new HotelCategoryConfiguration());

            modelBuilder.Configurations.Add(new RateConfiguration());
            modelBuilder.Configurations.Add(new RealCourseConfiguration());

            modelBuilder.Configurations.Add(new AirClassConfiguration());
            modelBuilder.Configurations.Add(new AircraftConfiguration());
            modelBuilder.Configurations.Add(new AirlineConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}