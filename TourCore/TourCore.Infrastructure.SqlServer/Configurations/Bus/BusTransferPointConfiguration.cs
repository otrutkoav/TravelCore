using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Bus
{
    public class BusTransferPointConfiguration : EntityTypeConfiguration<BusTransferPoint>
    {
        public BusTransferPointConfiguration()
        {
            ToTable("BusTransferPoints");

            HasKey(x => x.Id);

            Property(x => x.BusTransferId)
                .IsRequired();

            Property(x => x.CountryFromId)
                .IsRequired();

            Property(x => x.CityFromId)
                .IsRequired();

            Property(x => x.CountryToId)
                .IsRequired();

            Property(x => x.CityToId)
                .IsRequired();

            Property(x => x.TimeFrom)
                .IsOptional();

            Property(x => x.TimeTo)
                .IsOptional();

            Property(x => x.DayFrom)
                .IsOptional();

            Property(x => x.DayTo)
                .IsOptional();

            HasRequired(x => x.BusTransfer)
                .WithMany()
                .HasForeignKey(x => x.BusTransferId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.CountryFrom)
                .WithMany()
                .HasForeignKey(x => x.CountryFromId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.CityFrom)
                .WithMany()
                .HasForeignKey(x => x.CityFromId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.CountryTo)
                .WithMany()
                .HasForeignKey(x => x.CountryToId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.CityTo)
                .WithMany()
                .HasForeignKey(x => x.CityToId)
                .WillCascadeOnDelete(false);
        }
    }
}