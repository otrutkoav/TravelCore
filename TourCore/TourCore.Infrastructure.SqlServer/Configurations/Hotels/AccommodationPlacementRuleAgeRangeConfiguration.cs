using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Hotels
{
    public class AccommodationPlacementRuleAgeRangeConfiguration : EntityTypeConfiguration<AccommodationPlacementRuleAgeRange>
    {
        public AccommodationPlacementRuleAgeRangeConfiguration()
        {
            ToTable("AccommodationPlacementRuleAgeRanges");

            HasKey(x => x.Id);

            Property(x => x.AccommodationPlacementRuleId)
                .IsRequired();

            Property(x => x.AgeFrom)
                .IsRequired();

            Property(x => x.AgeTo)
                .IsRequired();

            HasRequired(x => x.AccommodationPlacementRule)
                .WithMany(x => x.ChildAgeRanges)
                .HasForeignKey(x => x.AccommodationPlacementRuleId)
                .WillCascadeOnDelete(false);
        }
    }
}