using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class PricePackageConfiguration : IEntityTypeConfiguration<PricePackage>
{
    public void Configure(EntityTypeBuilder<PricePackage> builder)
    {
        builder.ToTable("PricePackages");
        builder.HasKey(pricePackage => pricePackage.Id);
        builder.Property(pricePackage => pricePackage.Name)
            .HasMaxLength(PricePackage.MaximumNameLength)
            .IsRequired();
        builder.Property(pricePackage => pricePackage.CountryId)
            .IsRequired();
        builder.Property(pricePackage => pricePackage.Deleted)
            .HasDefaultValue(false)
            .IsRequired();
        builder.Property(pricePackage => pricePackage.IsDisabled)
            .HasDefaultValue(false)
            .IsRequired();
        builder.Property(pricePackage => pricePackage.CreatedByUserId)
            .HasMaxLength(PricePackage.MaximumCreatedByUserIdLength)
            .IsRequired();
        builder.Property(pricePackage => pricePackage.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        builder.Property(pricePackage => pricePackage.UpdatedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(pricePackage => new { pricePackage.CountryId, pricePackage.Name })
            .IsUnique()
            .HasFilter("NOT \"Deleted\"")
            .HasDatabaseName("UX_PricePackages_CountryId_Name_Active");
        builder.HasIndex(pricePackage => pricePackage.CountryId)
            .HasDatabaseName("IX_PricePackages_CountryId");
    }
}
