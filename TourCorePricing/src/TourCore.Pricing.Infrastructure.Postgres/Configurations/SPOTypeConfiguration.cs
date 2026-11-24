using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.SPOTypes;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class SPOTypeConfiguration : IEntityTypeConfiguration<SPOType>
{
    public void Configure(EntityTypeBuilder<SPOType> builder)
    {
        builder.ToTable("SPOTypes");
        builder.HasKey(spoType => spoType.Id);
        builder.Property(spoType => spoType.Name)
            .HasMaxLength(SPOType.MaximumNameLength)
            .IsRequired();
        builder.Property(spoType => spoType.NameLat)
            .HasMaxLength(SPOType.MaximumNameLength)
            .IsRequired();
        builder.Property(spoType => spoType.CreatedByUserId)
            .HasMaxLength(SPOType.MaximumUserIdLength)
            .IsRequired();
        builder.Property(spoType => spoType.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        builder.Property(spoType => spoType.UpdatedAt)
            .HasColumnType("timestamp with time zone");
        builder.HasIndex(spoType => spoType.Name)
            .IsUnique()
            .HasDatabaseName("UX_SPOTypes_Name");
    }
}
