using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Transfers
{
    public class TransferDirectionConfiguration : IEntityTypeConfiguration<TransferDirection>
    {
        public void Configure(EntityTypeBuilder<TransferDirection> builder)
        {
            builder.ToTable("TransferDirections");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}