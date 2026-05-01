using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Finance
{
    public class RateConfiguration : EntityTypeConfiguration<Rate>
    {
        public RateConfiguration()
        {
            ToTable("Rates");

            HasKey(x => x.Id);

            Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.IsoCode)
                .IsOptional()
                .HasMaxLength(3);

            Property(x => x.IsMain)
                .IsRequired();

            Property(x => x.IsNational)
                .IsRequired();

            Property(x => x.ShowInSearch)
                .IsRequired();

            Property(x => x.Symbol)
                .IsOptional();
        }
    }
}