using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Hotels
{
    public class AccommodationPlacementRuleAgeRangeConfiguration : IEntityTypeConfiguration<AccommodationPlacementRuleAgeRange>
    {
        public void Configure(EntityTypeBuilder<AccommodationPlacementRuleAgeRange> builder)
        {
            builder.ToTable("AccommodationPlacementRuleAgeRanges");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccommodationPlacementRuleId)
                .IsRequired();

            builder.Property(x => x.AgeFrom)
                .IsRequired();

            builder.Property(x => x.AgeTo)
                .IsRequired();

            builder.HasOne(x => x.AccommodationPlacementRule)
                .WithMany(x => x.ChildAgeRanges)
                .HasForeignKey(x => x.AccommodationPlacementRuleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}