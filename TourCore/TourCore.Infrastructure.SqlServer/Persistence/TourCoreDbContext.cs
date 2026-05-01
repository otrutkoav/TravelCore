using System.Data.Entity;
using TourCore.Domain.Geography.Entities;
using TourCore.Domain.Hotels.Entities;
using TourCore.Infrastructure.SqlServer.Configurations;

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new RegionConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ResortConfiguration());

            modelBuilder.Configurations.Add(new MealTypeConfiguration());
            modelBuilder.Configurations.Add(new RoomCategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}