using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Bus
{
    public class BusTransferPointConfiguration : IEntityTypeConfiguration<BusTransferPoint>
    {
        public void Configure(EntityTypeBuilder<BusTransferPoint> builder)
        {
            builder.ToTable("BusTransferPoints");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.BusTransferId)
                .IsRequired();

            builder.Property(x => x.CountryFromId)
                .IsRequired();

            builder.Property(x => x.CityFromId)
                .IsRequired();

            builder.Property(x => x.CountryToId)
                .IsRequired();

            builder.Property(x => x.CityToId)
                .IsRequired();

            builder.Property(x => x.TimeFrom)
                .IsRequired(false);

            builder.Property(x => x.TimeTo)
                .IsRequired(false);

            builder.Property(x => x.DayFrom)
                .IsRequired(false);

            builder.Property(x => x.DayTo)
                .IsRequired(false);

            builder.HasOne(x => x.BusTransfer)
                .WithMany()
                .HasForeignKey(x => x.BusTransferId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CountryFrom)
                .WithMany()
                .HasForeignKey(x => x.CountryFromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CityFrom)
                .WithMany()
                .HasForeignKey(x => x.CityFromId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CountryTo)
                .WithMany()
                .HasForeignKey(x => x.CountryToId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.CityTo)
                .WithMany()
                .HasForeignKey(x => x.CityToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}