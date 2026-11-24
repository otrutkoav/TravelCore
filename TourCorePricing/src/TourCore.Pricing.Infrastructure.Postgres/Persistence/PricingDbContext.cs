using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Domain.PricePackages;
using TourCore.Pricing.Domain.PriceBlocks;
using TourCore.Pricing.Domain.SPOTypes;
using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Infrastructure.Postgres.Persistence;

public sealed class PricingDbContext : DbContext
{
    public PricingDbContext(DbContextOptions<PricingDbContext> options)
        : base(options)
    {
    }

    public DbSet<PricePackage> PricePackages => Set<PricePackage>();
    public DbSet<PriceBlock> PriceBlocks => Set<PriceBlock>();
    public DbSet<SPOType> SPOTypes => Set<SPOType>();
    public DbSet<Season> Seasons => Set<Season>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PricingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
