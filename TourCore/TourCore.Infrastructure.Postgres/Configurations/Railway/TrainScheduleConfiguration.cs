using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TourCore.Domain.Railway.Entities;

namespace TourCore.Infrastructure.Postgres.Configurations.Railway
{
    public class TrainScheduleConfiguration : IEntityTypeConfiguration<TrainSchedule>
    {
        public void Configure(EntityTypeBuilder<TrainSchedule> builder)
        {
            builder.ToTable("TrainSchedules");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RailwayTransferId)
                .IsRequired();

            builder.Property(x => x.DateFrom)
                .IsRequired(false);

            builder.Property(x => x.DateTo)
                .IsRequired(false);

            builder.Property(x => x.TimeFrom)
                .IsRequired(false);

            builder.Property(x => x.TimeTo)
                .IsRequired(false);

            builder.Property(x => x.DaysOfWeekValue)
                .HasColumnName("DaysOfWeek")
                .IsRequired(false)
                .HasMaxLength(7);

            builder.Ignore(x => x.DaysOfWeek);

            builder.Property(x => x.DaysOnRoad)
                .IsRequired(false);

            builder.Property(x => x.Remark)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.HasOne(x => x.RailwayTransfer)
                .WithMany()
                .HasForeignKey(x => x.RailwayTransferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}