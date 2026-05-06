using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Hotels
{
    public class AccommodationPlacementRuleConfiguration : EntityTypeConfiguration<AccommodationPlacementRule>
    {
        public AccommodationPlacementRuleConfiguration()
        {
            ToTable("AccommodationPlacementRules");

            HasKey(x => x.Id);

            Property(x => x.AdultsCount)
                .IsRequired();

            Property(x => x.ChildrenCount)
                .IsRequired();

            Property(x => x.ChildrenAreInfants)
                .IsRequired();

            HasMany(x => x.ChildAgeRanges)
                .WithRequired(x => x.AccommodationPlacementRule)
                .HasForeignKey(x => x.AccommodationPlacementRuleId)
                .WillCascadeOnDelete(false);
        }
    }
}