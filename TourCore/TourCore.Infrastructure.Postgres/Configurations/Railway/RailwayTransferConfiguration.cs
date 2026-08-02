using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Railway
{
    public class RailwayTransferConfiguration : IEntityTypeConfiguration<RailwayTransfer>
    {
        public void Configure(EntityTypeBuilder<RailwayTransfer> builder)
        {
            builder.ToTable("RailwayTransfers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CountryFromId)
                .IsRequired();

            builder.Property(x => x.CityFromId)
                .IsRequired();

            builder.Property(x => x.CountryToId)
                .IsRequired();

            builder.Property(x => x.CityToId)
                .IsRequired();

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