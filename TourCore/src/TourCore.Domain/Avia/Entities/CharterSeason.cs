using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Common.ValueObjects;

namespace TourCore.Domain.Avia.Entities
{
    /// <summary>
    /// Сезон / расписание действия чартерного рейса.
    /// Определяет период действия, дни недели и время вылета/прилета.
    /// </summary>
    public class CharterSeason : AuditableEntity
    {
        public int CharterId { get; protected set; }

        public DateTime? DateFrom { get; protected set; }
        public DateTime? DateTo { get; protected set; }

        public DaysOfWeek DaysOfWeek { get; protected set; }

        public DateTime? TimeFrom { get; protected set; }
        public DateTime? TimeTo { get; protected set; }

        public bool IsNextDayArrival { get; protected set; }

        public string Remark { get; protected set; }

        protected CharterSeason()
        {
        }

        public CharterSeason(
            int charterId,
            DateTime createdAt,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            DaysOfWeek daysOfWeek = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            bool isNextDayArrival = false,
            string remark = null)
        {
            SetCharterId(charterId);
            SetDateRange(dateFrom, dateTo);
            SetDaysOfWeek(daysOfWeek);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetIsNextDayArrival(isNextDayArrival);
            SetRemark(remark);

            SetCreated(createdAt);
        }

        public void Update(
            int charterId,
            DateTime updatedAt,
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            DaysOfWeek daysOfWeek = null,
            DateTime? timeFrom = null,
            DateTime? timeTo = null,
            bool isNextDayArrival = false,
            string remark = null)
        {
            SetCharterId(charterId);
            SetDateRange(dateFrom, dateTo);
            SetDaysOfWeek(daysOfWeek);
            SetTimeFrom(timeFrom);
            SetTimeTo(timeTo);
            SetIsNextDayArrival(isNextDayArrival);
            SetRemark(remark);

            SetUpdated(updatedAt);
        }

        private void SetCharterId(int charterId)
        {
            if (charterId <= 0)
                throw new DomainException("Charter season charter id must be greater than zero.");

            CharterId = charterId;
        }

        private void SetDateRange(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateFrom.HasValue && dateTo.HasValue && dateFrom.Value.Date > dateTo.Value.Date)
                throw new DomainException("Charter season start date cannot be greater than end date.");

            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        private void SetDaysOfWeek(DaysOfWeek daysOfWeek)
        {
            DaysOfWeek = daysOfWeek;
        }

        private void SetTimeFrom(DateTime? timeFrom)
        {
            TimeFrom = timeFrom;
        }

        private void SetTimeTo(DateTime? timeTo)
        {
            TimeTo = timeTo;
        }

        private void SetIsNextDayArrival(bool isNextDayArrival)
        {
            IsNextDayArrival = isNextDayArrival;
        }

        private void SetRemark(string remark)
        {
            if (string.IsNullOrWhiteSpace(remark))
            {
                Remark = null;
                return;
            }

            remark = remark.Trim();

            if (remark.Length > 20)
                throw new DomainException("Charter season remark must be 20 characters or less.");

            Remark = remark;
        }
    }
}