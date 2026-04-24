using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Domain.Railway.Entities
{
    /// <summary>
    /// Расписание ЖД-переезда.
    /// Определяет период действия, дни недели и время отправления/прибытия.
    /// </summary>
    public class TrainSchedule : AuditableEntity
    {
        public int RailwayTransferId { get; protected set; }
        public virtual RailwayTransfer RailwayTransfer { get; protected set; }

        public DateTime? DateFrom { get; protected set; }
        public DateTime? DateTo { get; protected set; }

        public DateTime? TimeFrom { get; protected set; }
        public DateTime? TimeTo { get; protected set; }

        public DaysOfWeek DaysOfWeek { get; protected set; }

        public short? DaysOnRoad { get; protected set; }

        public string Remark { get; protected set; }

        protected TrainSchedule()
        {
        }

        public TrainSchedule(
            int railwayTransferId,
            DateTime createdAt,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            DaysOfWeek daysOfWeek = null,
            short? daysOnRoad = null,
            string remark = null)
        {
            SetRailwayTransferId(railwayTransferId);
            SetDateRange(dateFrom, dateTo);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDaysOfWeek(daysOfWeek);
            SetDaysOnRoad(daysOnRoad);
            SetRemark(remark);

            SetCreated(createdAt);
        }

        public void Update(
            int railwayTransferId,
            DateTime updatedAt,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            DaysOfWeek daysOfWeek = null,
            short? daysOnRoad = null,
            string remark = null)
        {
            SetRailwayTransferId(railwayTransferId);
            SetDateRange(dateFrom, dateTo);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetDaysOfWeek(daysOfWeek);
            SetDaysOnRoad(daysOnRoad);
            SetRemark(remark);

            SetUpdated(updatedAt);
        }

        private void SetRailwayTransferId(int value)
        {
            if (value <= 0)
                throw new DomainException("Train schedule railway transfer id must be greater than zero.");

            RailwayTransferId = value;
        }

        private void SetDateRange(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom.HasValue && dateTo.HasValue && dateFrom.Value.Date > dateTo.Value.Date)
                throw new DomainException("Train schedule start date cannot be greater than end date.");

            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        private void SetTimeFrom(DateTime? value)
        {
            TimeFrom = value;
        }

        private void SetTimeTo(DateTime? value)
        {
            TimeTo = value;
        }

        private void SetDaysOfWeek(DaysOfWeek value)
        {
            DaysOfWeek = value;
        }

        private void SetDaysOnRoad(short? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Train schedule days on road cannot be negative.");

            DaysOnRoad = value;
        }

        private void SetRemark(string remark)
        {
            if (string.IsNullOrWhiteSpace(remark))
            {
                Remark = null;
                return;
            }

            remark = remark.Trim();

            if (remark.Length > 100)
                throw new DomainException("Train schedule remark must be 100 characters or less.");

            Remark = remark;
        }
    }
}