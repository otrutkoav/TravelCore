using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.HotelPrices;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class HotelPriceComponentConfiguration : IEntityTypeConfiguration<HotelPriceComponent>
{
    public void Configure(EntityTypeBuilder<HotelPriceComponent> builder)
    {
        builder.ToTable("HotelPriceComponents", table =>
        {
            table.HasCheckConstraint("CK_HotelPriceComponents_Ages", "(\"AgeFrom\" IS NULL OR \"AgeFrom\" >= 0) AND (\"AgeTo\" IS NULL OR \"AgeTo\" >= 0) AND (\"AgeFrom\" IS NULL OR \"AgeTo\" IS NULL OR \"AgeFrom\" <= \"AgeTo\")");
            table.HasCheckConstraint("CK_HotelPriceComponents_Amounts", "(\"NettoAmount\" IS NULL OR \"NettoAmount\" >= 0) AND (\"BruttoAmount\" IS NULL OR \"BruttoAmount\" >= 0)");
            table.HasCheckConstraint("CK_HotelPriceComponents_Percent", "\"Percent\" IS NULL OR (\"Percent\" > 0 AND \"Percent\" <= 1000)");
        });
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type).HasConversion<byte>().IsRequired();
        builder.Property(x => x.GuestType).HasConversion<byte?>();
        builder.Property(x => x.PlacementType).HasConversion<byte>().IsRequired();
        builder.Property(x => x.Operation).HasConversion<byte>().IsRequired();
        builder.Property(x => x.ApplicationUnit).HasConversion<byte>().IsRequired();
        builder.Property(x => x.PercentageBase).HasConversion<byte?>();
        builder.Property(x => x.NettoAmount).HasPrecision(18, 4);
        builder.Property(x => x.BruttoAmount).HasPrecision(18, 4);
        builder.Property(x => x.Percent).HasPrecision(9, 4);
        builder.Property(x => x.CreatedByUserId).HasMaxLength(HotelPriceRule.MaximumUserIdLength).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone");
        builder.HasOne<HotelPriceRule>().WithMany().HasForeignKey(x => x.HotelPriceRuleId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(x => x.HotelPriceRuleId).HasDatabaseName("IX_HotelPriceComponents_RuleId");
    }
}
