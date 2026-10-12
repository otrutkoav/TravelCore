using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Finance.Entities
{
    /// <summary>
    /// Курс одной валюты к другой на заданный период.
    /// </summary>
    public class RealCourse : AuditableEntity
    {
        public string FromRateCode { get; protected set; }
        public virtual Rate FromRate { get; protected set; }

        public string ToRateCode { get; protected set; }
        public virtual Rate ToRate { get; protected set; }

        public decimal? Course { get; protected set; }
        public decimal? CentralBankCourse { get; protected set; }

        public DateTime? DateBeg { get; protected set; }
        public DateTime? DateEnd { get; protected set; }

        protected RealCourse()
        {
        }

        public RealCourse(
            string fromRateCode,
            string toRateCode,
            DateTime createdAt,
            decimal? course = null,
            decimal? centralBankCourse = null,
            DateTime? dateBeg = null,
            DateTime? dateEnd = null)
        {
            SetFromRateCode(fromRateCode);
            SetToRateCode(toRateCode);
            SetCourse(course);
            SetCentralBankCourse(centralBankCourse);
            SetDateRange(dateBeg, dateEnd);

            SetCreated(createdAt);
        }

        public void Update(
            string fromRateCode,
            string toRateCode,
            DateTime updatedAt,
            decimal? course = null,
            decimal? centralBankCourse = null,
            DateTime? dateBeg = null,
            DateTime? dateEnd = null)
        {
            SetFromRateCode(fromRateCode);
            SetToRateCode(toRateCode);
            SetCourse(course);
            SetCentralBankCourse(centralBankCourse);
            SetDateRange(dateBeg, dateEnd);

            SetUpdated(updatedAt);
        }

        private void SetFromRateCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Real course source rate code is required.");

            value = value.Trim();

            if (value.Length > 3)
                throw new DomainException("Real course source rate code must be 3 characters or less.");

            FromRateCode = value;
        }

        private void SetToRateCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Real course target rate code is required.");

            value = value.Trim();

            if (value.Length > 3)
                throw new DomainException("Real course target rate code must be 3 characters or less.");

            ToRateCode = value;
        }

        private void SetCourse(decimal? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Real course value cannot be negative.");

            Course = value;
        }

        private void SetCentralBankCourse(decimal? value)
        {
            if (value.HasValue && value.Value < 0)
                throw new DomainException("Real course central bank value cannot be negative.");

            CentralBankCourse = value;
        }

        private void SetDateRange(DateTime? dateBeg, DateTime? dateEnd)
        {
            if (dateBeg.HasValue && dateEnd.HasValue && dateBeg.Value.Date > dateEnd.Value.Date)
                throw new DomainException("Real course start date cannot be greater than end date.");

            DateBeg = dateBeg;
            DateEnd = dateEnd;
        }
    }
}