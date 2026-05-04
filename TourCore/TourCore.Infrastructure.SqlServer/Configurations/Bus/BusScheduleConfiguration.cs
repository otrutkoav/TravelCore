using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Bus.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Bus
{
    public class BusScheduleConfiguration : EntityTypeConfiguration<BusSchedule>
    {
        public BusScheduleConfiguration()
        {
            ToTable("BusSchedules");

            HasKey(x => x.Id);

            Property(x => x.BusTransferId)
                .IsRequired();

            Property(x => x.DateFrom)
                .IsOptional();

            Property(x => x.DateTo)
                .IsOptional();

            Property(x => x.TimeFrom)
                .IsOptional();

            Property(x => x.TimeTo)
                .IsOptional();

            Property(x => x.DaysOfWeekValue)
             .HasColumnName("DaysOfWeek")
             .IsOptional()
             .HasMaxLength(7);

            Property(x => x.DaysOnRoad)
                .IsOptional();

            Ignore(x => x.DaysOfWeek);

            HasRequired(x => x.BusTransfer)
                .WithMany()
                .HasForeignKey(x => x.BusTransferId)
                .WillCascadeOnDelete(false);
        }
    }
}