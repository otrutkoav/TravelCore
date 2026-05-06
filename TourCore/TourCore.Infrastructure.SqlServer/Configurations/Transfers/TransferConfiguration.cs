using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Transfers
{
    public class TransferConfiguration : EntityTypeConfiguration<Transfer>
    {
        public TransferConfiguration()
        {
            ToTable("Transfers");

            HasKey(x => x.Id);

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.NameEn)
                .IsOptional()
                .HasMaxLength(100);

            Property(x => x.TimeFrom)
                .IsOptional();

            Property(x => x.TimeTo)
                .IsOptional();

            Property(x => x.DurationText)
                .IsOptional()
                .HasMaxLength(5);

            Property(x => x.PlaceFrom)
                .IsOptional()
                .HasMaxLength(300);

            Property(x => x.PlaceTo)
                .IsOptional()
                .HasMaxLength(300);

            Property(x => x.IsMain)
                .IsRequired();

            Property(x => x.Url)
                .IsOptional()
                .HasMaxLength(192);

            Property(x => x.ShowOrder)
                .IsRequired();

            Property(x => x.AutoApplyFrom)
                .IsRequired();

            Property(x => x.AutoApplyTo)
                .IsRequired();

            Property(x => x.CityId)
                .IsOptional();

            Property(x => x.DirectionId)
                .IsOptional();

            HasOptional(x => x.City)
                .WithMany()
                .HasForeignKey(x => x.CityId)
                .WillCascadeOnDelete(false);

            HasOptional(x => x.Direction)
                .WithMany()
                .HasForeignKey(x => x.DirectionId)
                .WillCascadeOnDelete(false);
        }
    }
}