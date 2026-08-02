using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Transportation
{
    public class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.ToTable("Transports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NameEn)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(x => x.SeatsCount)
                .IsRequired(false);

            builder.Property(x => x.ShowOrder)
                .IsRequired();
        }
    }
}