using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Railway
{
    public class RailwayTransferConfiguration : EntityTypeConfiguration<RailwayTransfer>
    {
        public RailwayTransferConfiguration()
        {
            ToTable("RailwayTransfers");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.CountryFromId)
                .IsRequired();

            Property(x => x.CityFromId)
                .IsRequired();

            Property(x => x.CountryToId)
                .IsRequired();

            Property(x => x.CityToId)
                .IsRequired();

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