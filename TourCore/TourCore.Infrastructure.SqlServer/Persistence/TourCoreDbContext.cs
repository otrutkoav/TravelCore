using System.Data.Entity;
using TourCore.Domain.Geography.Entities;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CountryConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}