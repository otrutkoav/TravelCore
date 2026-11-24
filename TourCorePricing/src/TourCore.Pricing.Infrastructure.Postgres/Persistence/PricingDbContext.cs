using Microsoft.EntityFrameworkCore;
using TourCore.Pricing.Domain.PricePackages;
using TourCore.Pricing.Domain.PriceBlocks;
using TourCore.Pricing.Domain.SPOTypes;
using TourCore.Pricing.Domain.Seasons;
using TourCore.Pricing.Domain.HotelPrices;
using TourCore.Pricing.Domain.FlightPrices;
using TourCore.Pricing.Domain.BusPrices;
using TourCore.Pricing.Domain.RailwayPrices;

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
    public DbSet<HotelPriceRule> HotelPriceRules => Set<HotelPriceRule>();
    public DbSet<HotelPriceComponent> HotelPriceComponents => Set<HotelPriceComponent>();
    public DbSet<HotelMealPriceRule> HotelMealPriceRules => Set<HotelMealPriceRule>();
    public DbSet<FlightPrice> FlightPrices => Set<FlightPrice>();
    public DbSet<BusTransferPrice> BusTransferPrices => Set<BusTransferPrice>();
    public DbSet<RailwayTransferPrice> RailwayTransferPrices => Set<RailwayTransferPrice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PricingDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
