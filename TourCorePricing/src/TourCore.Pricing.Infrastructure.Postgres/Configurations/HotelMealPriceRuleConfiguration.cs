using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.HotelPrices;
using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class HotelMealPriceRuleConfiguration : IEntityTypeConfiguration<HotelMealPriceRule>
{
    public void Configure(EntityTypeBuilder<HotelMealPriceRule> builder)
    {
        builder.ToTable("HotelMealPriceRules", table =>
        {
            table.HasCheckConstraint("CK_HotelMealPriceRules_StayDates", "\"StayDateFrom\" <= \"StayDateTo\"");
            table.HasCheckConstraint("CK_HotelMealPriceRules_CheckInDates", "(\"CheckInDateFrom\" IS NULL AND \"CheckInDateTo\" IS NULL) OR (\"CheckInDateFrom\" <= \"CheckInDateTo\")");
            table.HasCheckConstraint("CK_HotelMealPriceRules_DaysOfWeek", "\"DaysOfWeekMask\" BETWEEN 1 AND 127");
            table.HasCheckConstraint("CK_HotelMealPriceRules_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
            table.HasCheckConstraint("CK_HotelMealPriceRules_Amounts", "(\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
            table.HasCheckConstraint("CK_HotelMealPriceRules_Priority", "\"Priority\" >= 0");
            table.HasCheckConstraint("CK_HotelMealPriceRules_Meals", "\"FromMealTypeId\" IS NULL OR \"FromMealTypeId\" <> \"ToMealTypeId\"");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.GuestType).HasConversion<byte?>();
        builder.Property(x => x.PlacementType).HasConversion<byte>().IsRequired();
        builder.Property(x => x.ApplicationUnit).HasConversion<byte>().IsRequired();
        builder.Property(x => x.Operation).HasConversion<byte>().IsRequired();
        builder.Property(x => x.PercentageBase).HasConversion<byte?>();
        builder.Property(x => x.NettoAmount).HasPrecision(18, 4);
        builder.Property(x => x.BruttoAmount).HasPrecision(18, 4);
        builder.Property(x => x.Percent).HasPrecision(9, 4);
        builder.Property(x => x.CreatedByUserId).HasMaxLength(HotelPriceRule.MaximumUserIdLength).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
        builder.HasOne<PriceBlock>().WithMany().HasForeignKey(x => x.PriceBlockId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => new { x.PriceBlockId, x.HotelId }).HasDatabaseName("IX_HotelMealPriceRules_Block_Hotel");
        builder.HasIndex(x => new { x.HotelId, x.FromMealTypeId, x.ToMealTypeId, x.StayDateFrom, x.StayDateTo })
            .HasDatabaseName("IX_HotelMealPriceRules_Search");
    }
}
