using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.HotelPrices;
using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class HotelPriceRuleConfiguration : IEntityTypeConfiguration<HotelPriceRule>
{
    public void Configure(EntityTypeBuilder<HotelPriceRule> builder)
    {
        builder.ToTable("HotelPriceRules", table =>
        {
            table.HasCheckConstraint("CK_HotelPriceRules_StayDates", "\"StayDateFrom\" <= \"StayDateTo\"");
            table.HasCheckConstraint("CK_HotelPriceRules_CheckInDates", "(\"CheckInDateFrom\" IS NULL AND \"CheckInDateTo\" IS NULL) OR (\"CheckInDateFrom\" <= \"CheckInDateTo\")");
            table.HasCheckConstraint("CK_HotelPriceRules_DaysOfWeek", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
            table.HasCheckConstraint("CK_HotelPriceRules_Nights", "(\"MinNights\" IS NULL OR \"MinNights\" > 0) AND (\"MaxNights\" IS NULL OR \"MaxNights\" > 0) AND (\"MinNights\" IS NULL OR \"MaxNights\" IS NULL OR \"MinNights\" <= \"MaxNights\")");
            table.HasCheckConstraint("CK_HotelPriceRules_Amounts", "(\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
            table.HasCheckConstraint("CK_HotelPriceRules_Priority", "\"Priority\" >= 0");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Model).HasConversion<byte>().IsRequired();
        builder.Property(x => x.AgeCalculationDate).HasConversion<byte>().IsRequired();
        builder.Property(x => x.ApplicationUnit).HasConversion<byte>().IsRequired();
        builder.Property(x => x.NettoAmount).HasPrecision(18, 4);
        builder.Property(x => x.BruttoAmount).HasPrecision(18, 4);
        builder.Property(x => x.CreatedByUserId).HasMaxLength(HotelPriceRule.MaximumUserIdLength).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
        builder.HasOne<PriceBlock>().WithMany().HasForeignKey(x => x.PriceBlockId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.PriceBlockId, x.HotelId }).HasDatabaseName("IX_HotelPriceRules_Block_Hotel");
        builder.HasIndex(x => new { x.HotelId, x.RoomTypeId, x.RoomCategoryId, x.StayDateFrom, x.StayDateTo })
            .HasDatabaseName("IX_HotelPriceRules_Search");
    }
}
