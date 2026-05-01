using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations
{
    public class ResortConfiguration : EntityTypeConfiguration<Resort>
    {
        public ResortConfiguration()
        {
            ToTable("Resorts");

            HasKey(x => x.Id);

            Property(x => x.CountryId)
                .IsRequired();

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            HasRequired(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .WillCascadeOnDelete(false);
        }
    }
}