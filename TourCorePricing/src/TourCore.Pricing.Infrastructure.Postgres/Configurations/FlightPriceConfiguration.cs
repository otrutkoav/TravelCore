using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.FlightPrices;
using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class FlightPriceConfiguration : IEntityTypeConfiguration<FlightPrice>
{
    public void Configure(EntityTypeBuilder<FlightPrice> builder)
    {
        builder.ToTable("FlightPrices", table =>
        {
            table.HasCheckConstraint("CK_FlightPrices_TravelDates", "\"TravelDateFrom\" <= \"TravelDateTo\"");
            table.HasCheckConstraint("CK_FlightPrices_SaleDates", "\"SaleDateFrom\" IS NULL OR \"SaleDateTo\" IS NULL OR \"SaleDateFrom\" <= \"SaleDateTo\"");
            table.HasCheckConstraint("CK_FlightPrices_DaysOfWeek", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
            table.HasCheckConstraint("CK_FlightPrices_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
            table.HasCheckConstraint("CK_FlightPrices_Amounts", "(\"NettoAmount\" IS NOT NULL OR \"BruttoAmount\" IS NOT NULL) AND (\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
            table.HasCheckConstraint("CK_FlightPrices_Priority", "\"Priority\" >= 0");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NettoAmount).HasPrecision(18, 4);
        builder.Property(x => x.BruttoAmount).HasPrecision(18, 4);
        builder.Property(x => x.CreatedByUserId).HasMaxLength(FlightPrice.MaximumUserIdLength).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
        builder.HasOne<PriceBlock>().WithMany().HasForeignKey(x => x.PriceBlockId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.PriceBlockId, x.CharterId, x.AirClassId })
            .HasDatabaseName("IX_FlightPrices_Block_Charter_AirClass");
        builder.HasIndex(x => new { x.CharterId, x.AirClassId, x.TravelDateFrom, x.TravelDateTo })
            .HasDatabaseName("IX_FlightPrices_Search");
    }
}
