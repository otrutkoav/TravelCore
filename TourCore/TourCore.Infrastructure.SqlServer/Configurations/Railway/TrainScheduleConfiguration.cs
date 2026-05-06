using System.Data.Entity.ModelConfiguration;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Infrastructure.SqlServer.Configurations.Railway
{
    public class TrainScheduleConfiguration : EntityTypeConfiguration<TrainSchedule>
    {
        public TrainScheduleConfiguration()
        {
            ToTable("TrainSchedules");

            HasKey(x => x.Id);

            Property(x => x.RailwayTransferId)
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

            Ignore(x => x.DaysOfWeek);

            Property(x => x.DaysOnRoad)
                .IsOptional();

            Property(x => x.Remark)
                .IsOptional()
                .HasMaxLength(100);

            HasRequired(x => x.RailwayTransfer)
                .WithMany()
                .HasForeignKey(x => x.RailwayTransferId)
                .WillCascadeOnDelete(false);
        }
    }
}