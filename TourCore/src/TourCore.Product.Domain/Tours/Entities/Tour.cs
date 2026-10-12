using System;
using System.Collections.Generic;
using System.Linq;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class Tour : AuditableEntity
    {
        public string Name { get; protected set; }
        public TourStatus Status { get; protected set; }
        public long Revision { get; protected set; }
        public int CurrencyId { get; protected set; }
        public TourDateMode DateMode { get; protected set; }
        public DateTime? AvailableDateFrom { get; protected set; }
        public DateTime? AvailableDateTo { get; protected set; }
        public string DescriptionUrl { get; protected set; }
        public string DescriptionHtml { get; protected set; }
        public string BookingConditionsHtml { get; protected set; }
        public virtual ICollection<TourAvailableDate> AvailableDates { get; protected set; }
        public virtual ICollection<TourType> TourTypes { get; protected set; }
        public virtual ICollection<TourService> Services { get; protected set; }

        protected Tour()
        {
            AvailableDates = new List<TourAvailableDate>();
            TourTypes = new List<TourType>();
            Services = new List<TourService>();
        }

        public Tour(
            string name,
            int currencyId,
            TourDateMode dateMode,
            DateTime? from,
            DateTime? to,
            IEnumerable<DateTime> dates,
            string url,
            string description,
            string conditions,
            DateTime createdAt
        )
            : this()
        {
            SetName(name);
            SetCurrency(currencyId);
            Status = TourStatus.New;
            Revision = 1;
            SetCalendar(dateMode, from, to, dates);
            SetContent(url, description, conditions);
            SetCreated(createdAt);
        }

        public void Update(
            string name,
            TourStatus status,
            int currencyId,
            TourDateMode dateMode,
            DateTime? from,
            DateTime? to,
            IEnumerable<DateTime> dates,
            string url,
            string description,
            string conditions,
            DateTime updatedAt
        )
        {
            SetName(name);
            SetStatus(status);
            SetCurrency(currencyId);
            SetCalendar(dateMode, from, to, dates);
            SetContent(url, description, conditions);
            SetUpdated(updatedAt);
        }

        public void ReplaceTourTypes(IEnumerable<TourType> types)
        {
            TourTypes.Clear();
            foreach (var type in types ?? Enumerable.Empty<TourType>())
                TourTypes.Add(type);
        }

        public void IncrementRevision()
        {
            checked
            {
                Revision++;
            }
        }

        private void SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Tour name is required.");
            value = value.Trim();
            if (value.Length > 128)
                throw new DomainException("Tour name must be 128 characters or less.");
            Name = value;
        }

        private void SetCurrency(int value)
        {
            if (value <= 0)
                throw new DomainException("Currency is required.");
            CurrencyId = value;
        }

        private void SetStatus(TourStatus value)
        {
            if (!Enum.IsDefined(typeof(TourStatus), value))
                throw new DomainException("Tour status is invalid.");
            Status = value;
        }

        private void SetCalendar(
            TourDateMode mode,
            DateTime? from,
            DateTime? to,
            IEnumerable<DateTime> dates
        )
        {
            if (!Enum.IsDefined(typeof(TourDateMode), mode))
                throw new DomainException("Tour date mode is invalid.");
            DateMode = mode;
            AvailableDates.Clear();
            if (mode == TourDateMode.ContinuousPeriod)
            {
                if (!from.HasValue || !to.HasValue)
                    throw new DomainException("Available date range is required.");
                if (from.Value.Date > to.Value.Date)
                    throw new DomainException(
                        "AvailableDateFrom must not be later than AvailableDateTo."
                    );
                AvailableDateFrom = from.Value.Date;
                AvailableDateTo = to.Value.Date;
            }
            else
            {
                var values = (dates ?? Enumerable.Empty<DateTime>())
                    .Select(x => x.Date)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList();
                if (values.Count == 0)
                    throw new DomainException("At least one available date is required.");
                AvailableDateFrom = null;
                AvailableDateTo = null;
                foreach (var date in values)
                    AvailableDates.Add(new TourAvailableDate(date));
            }
        }

        private void SetContent(string url, string description, string conditions)
        {
            if (!string.IsNullOrWhiteSpace(url) && url.Trim().Length > 2048)
                throw new DomainException("Description URL must be 2048 characters or less.");
            DescriptionUrl = string.IsNullOrWhiteSpace(url) ? null : url.Trim();
            DescriptionHtml = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            BookingConditionsHtml = string.IsNullOrWhiteSpace(conditions)
                ? null
                : conditions.Trim();
        }
    }
}
