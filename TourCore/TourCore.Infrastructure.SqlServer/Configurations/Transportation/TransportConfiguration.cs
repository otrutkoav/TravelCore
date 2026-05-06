using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Transportation
{
    public class TransportConfiguration : EntityTypeConfiguration<Transport>
    {
        public TransportConfiguration()
        {
            ToTable("Transports");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.SeatsCount)
                .IsOptional();

            Property(x => x.ShowOrder)
                .IsRequired();
        }
    }
}