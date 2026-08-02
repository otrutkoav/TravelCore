using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Hotels.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Hotels
{
    public class AccommodationTypeConfiguration : IEntityTypeConfiguration<AccommodationType>
    {
        public void Configure(EntityTypeBuilder<AccommodationType> builder)
        {
            builder.ToTable("AccommodationTypes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.IsMain)
                .IsRequired();

            builder.Property(x => x.AgeFrom)
                .IsRequired(false);

            builder.Property(x => x.AgeTo)
                .IsRequired(false);

            builder.Property(x => x.PerRoom)
                .IsRequired(false);

            builder.Property(x => x.SortOrder)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired(false);

            builder.Property(x => x.MainPlacementRuleId)
                .IsRequired(false);

            builder.Property(x => x.ExtraPlacementRuleId)
                .IsRequired(false);

            builder.HasOne(x => x.MainPlacementRule)
                .WithMany()
                .HasForeignKey(x => x.MainPlacementRuleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ExtraPlacementRule)
                .WithMany()
                .HasForeignKey(x => x.ExtraPlacementRuleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}