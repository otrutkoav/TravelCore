using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.PriceBlocks;
using TourCore.Pricing.Domain.RailwayPrices;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class RailwayTransferPriceConfiguration : IEntityTypeConfiguration<RailwayTransferPrice>
{
    public void Configure(EntityTypeBuilder<RailwayTransferPrice> builder)
    {
        builder.ToTable("RailwayTransferPrices", table =>
        {
            table.HasCheckConstraint("CK_RailwayTransferPrices_TravelDates", "\"TravelDateFrom\" IS NULL OR \"TravelDateTo\" IS NULL OR \"TravelDateFrom\" <= \"TravelDateTo\"");
            table.HasCheckConstraint("CK_RailwayTransferPrices_SaleDates", "\"SaleDateFrom\" IS NULL OR \"SaleDateTo\" IS NULL OR \"SaleDateFrom\" <= \"SaleDateTo\"");
            table.HasCheckConstraint("CK_RailwayTransferPrices_Days", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
            table.HasCheckConstraint("CK_RailwayTransferPrices_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
            table.HasCheckConstraint("CK_RailwayTransferPrices_Amounts", "(\"NettoAmount\" IS NOT NULL OR \"BruttoAmount\" IS NOT NULL) AND (\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
            table.HasCheckConstraint("CK_RailwayTransferPrices_Priority", "\"Priority\" >= 0");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Scope).HasConversion<byte>().IsRequired();
        builder.Property(x => x.PeriodUnit).HasConversion<byte>().IsRequired();
        builder.Property(x => x.NettoAmount).HasPrecision(18, 4);
        builder.Property(x => x.BruttoAmount).HasPrecision(18, 4);
        builder.Property(x => x.CreatedByUserId).HasMaxLength(RailwayTransferPrice.MaximumUserIdLength).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
        builder.HasOne<PriceBlock>().WithMany().HasForeignKey(x => x.PriceBlockId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.PriceBlockId, x.RailwayTransferId, x.TransportId }).HasDatabaseName("IX_RailwayTransferPrices_Block_Transfer_Transport");
        builder.HasIndex(x => new { x.RailwayTransferId, x.TransportId, x.TravelDateFrom, x.TravelDateTo }).HasDatabaseName("IX_RailwayTransferPrices_Search");
    }
}
