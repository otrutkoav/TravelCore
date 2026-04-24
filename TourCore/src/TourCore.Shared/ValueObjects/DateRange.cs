using System;
using System.Collections.Generic;
using TourCore.Shared.Primitives;

namespace TourCore.Shared.ValueObjects
{
    public sealed class DateRange : ValueObject
    {
        public DateTime StartDate { get; private set; }

        public DateTime EndDate { get; private set; }

        private DateRange()
        {
        }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
                throw new ArgumentException("End date cannot be earlier than start date.");

            StartDate = startDate.Date;
            EndDate = endDate.Date;
        }

        public int Days
        {
            get { return (EndDate - StartDate).Days + 1; }
        }

        public bool Contains(DateTime date)
        {
            var value = date.Date;

            return value >= StartDate && value <= EndDate;
        }

        public bool Overlaps(DateRange other)
        {
            if (other == null)
                return false;

            return StartDate <= other.EndDate && EndDate >= other.StartDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }
    }
}