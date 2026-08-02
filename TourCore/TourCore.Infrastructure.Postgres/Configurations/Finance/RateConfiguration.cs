using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Finance.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Finance
{
    public class RateConfiguration : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.ToTable("Rates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.IsoCode)
                .IsRequired(false)
                .HasMaxLength(3);

            builder.Property(x => x.IsMain)
                .IsRequired();

            builder.Property(x => x.IsNational)
                .IsRequired();

            builder.Property(x => x.ShowInSearch)
                .IsRequired();

            builder.Property(x => x.Symbol)
                .IsRequired(false);
        }
    }
}