using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Transfers
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> builder)
        {
            builder.ToTable("Transfers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.TimeFrom)
                .IsRequired(false);

            builder.Property(x => x.TimeTo)
                .IsRequired(false);

            builder.Property(x => x.DurationText)
                .IsRequired(false)
                .HasMaxLength(5);

            builder.Property(x => x.PlaceFrom)
                .IsRequired(false)
                .HasMaxLength(300);

            builder.Property(x => x.PlaceTo)
                .IsRequired(false)
                .HasMaxLength(300);

            builder.Property(x => x.IsMain)
                .IsRequired();

            builder.Property(x => x.Url)
                .IsRequired(false)
                .HasMaxLength(192);

            builder.Property(x => x.ShowOrder)
                .IsRequired();

            builder.Property(x => x.AutoApplyFrom)
                .IsRequired();

            builder.Property(x => x.AutoApplyTo)
                .IsRequired();

            builder.Property(x => x.CityId)
                .IsRequired(false);

            builder.Property(x => x.DirectionId)
                .IsRequired(false);

            builder.HasOne(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Direction)
                .WithMany()
                .HasForeignKey(x => x.DirectionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}