using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Hotels
{
    public class AccommodationTypeConfiguration : EntityTypeConfiguration<AccommodationType>
    {
        public AccommodationTypeConfiguration()
        {
            ToTable("AccommodationTypes");

            HasKey(x => x.Id);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.IsMain)
                .IsRequired();

            Property(x => x.AgeFrom)
                .IsOptional();

            Property(x => x.AgeTo)
                .IsOptional();

            Property(x => x.PerRoom)
                .IsOptional();

            Property(x => x.SortOrder)
                .IsRequired();

            Property(x => x.Description)
                .IsOptional();

            Property(x => x.MainPlacementRuleId)
                .IsOptional();

            Property(x => x.ExtraPlacementRuleId)
                .IsOptional();

            HasOptional(x => x.MainPlacementRule)
                .WithMany()
                .HasForeignKey(x => x.MainPlacementRuleId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.ExtraPlacementRule)
                .WithMany()
                .HasForeignKey(x => x.ExtraPlacementRuleId)
                .WillCascadeOnDelete(false);
        }
    }
}