using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Infrastructure.Postgres.Configurations;

internal sealed class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.ToTable("Seasons");
        builder.HasKey(season => season.Id);
        builder.Property(season => season.Id).UseIdentityByDefaultColumn();
        builder.Property(season => season.Name)
            .HasMaxLength(Season.MaximumNameLength)
            .IsRequired();
        builder.Property(season => season.NameLat)
            .HasMaxLength(Season.MaximumNameLength)
            .IsRequired();
        builder.Property(season => season.CreatedByUserId)
            .HasMaxLength(Season.MaximumUserIdLength)
            .IsRequired();
        builder.Property(season => season.CreatedAt)
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        builder.Property(season => season.UpdatedAt)
            .HasColumnType("timestamp with time zone");

        builder.HasIndex(season => season.Name)
            .IsUnique()
            .HasDatabaseName("UX_Seasons_Name");
        builder.HasIndex(season => season.IsMain)
            .IsUnique()
            .HasFilter("\"IsMain\"")
            .HasDatabaseName("UX_Seasons_OnlyOneMain");
    }
}
