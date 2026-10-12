using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Geography.Entities
{
    public class Resort : AuditableEntity
    {
        public int CountryId { get; protected set; }
        public virtual Country Country { get; protected set; }

        public string Name { get; protected set; }
        public string NameEn { get; protected set; }

        protected Resort()
        {
        }

        public Resort(
            int countryId,
            string name,
            DateTime createdAt,
            string nameEn = null)
        {
            SetCountryId(countryId);
            SetName(name);
            SetNameEn(nameEn);

            SetCreated(createdAt);
        }

        public void Update(
            int countryId,
            string name,
            DateTime updatedAt,
            string nameEn = null)
        {
            SetCountryId(countryId);
            SetName(name);
            SetNameEn(nameEn);

            SetUpdated(updatedAt);
        }

        private void SetCountryId(int countryId)
        {
            if (countryId <= 0)
                throw new DomainException("Resort country id must be greater than zero.");

            CountryId = countryId;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Resort name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Resort name must be 100 characters or less.");

            Name = name;
        }

        private void SetNameEn(string nameEn)
        {
            if (string.IsNullOrWhiteSpace(nameEn))
            {
                NameEn = null;
                return;
            }

            nameEn = nameEn.Trim();

            if (nameEn.Length > 100)
                throw new DomainException("Resort alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }
    }
}