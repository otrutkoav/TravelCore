using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Hotels
{
    public class AccommodationPlacementRuleConfiguration : IEntityTypeConfiguration<AccommodationPlacementRule>
    {
        public void Configure(EntityTypeBuilder<AccommodationPlacementRule> builder)
        {
            builder.ToTable("AccommodationPlacementRules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AdultsCount)
                .IsRequired();

            builder.Property(x => x.ChildrenCount)
                .IsRequired();

            builder.Property(x => x.ChildrenAreInfants)
                .IsRequired();

            builder.HasMany(x => x.ChildAgeRanges)
                .WithOne(x => x.AccommodationPlacementRule)
                .HasForeignKey(x => x.AccommodationPlacementRuleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}