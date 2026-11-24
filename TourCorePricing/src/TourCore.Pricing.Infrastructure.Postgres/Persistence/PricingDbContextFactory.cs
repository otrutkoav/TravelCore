using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

public sealed class PricingDbContextFactory : IDesignTimeDbContextFactory<PricingDbContext>
{
    public PricingDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__TourCoreDb")
            ?? "Host=localhost;Port=5432;Database=tourcore_admin;Username=tourcore_admin;Password=tourcore_admin";
        var options = new DbContextOptionsBuilder<PricingDbContext>()
            .UseNpgsql(connectionString)
            .Options;
        return new PricingDbContext(options);
    }
}
