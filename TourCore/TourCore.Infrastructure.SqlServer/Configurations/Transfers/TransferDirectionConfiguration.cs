using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Transfers
{
    public class TransferDirectionConfiguration : EntityTypeConfiguration<TransferDirection>
    {
        public TransferDirectionConfiguration()
        {
            ToTable("TransferDirections");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);
        }
    }
}