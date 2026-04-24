using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Geography.Entities
{
    public class Region : AuditableEntity
    {
        public int CountryId { get; protected set; }
        public virtual Country Country { get; protected set; }

        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string Code { get; protected set; }
        public int SortOrder { get; protected set; }

        protected Region()
        {
        }

        public Region(
            int countryId,
            string name,
            DateTime createdAt,
            string nameEn = null,
            string code = null,
            int sortOrder = 0)
        {
            SetCountryId(countryId);
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetSortOrder(sortOrder);

            SetCreated(createdAt);
        }

        public void Update(
            int countryId,
            string name,
            DateTime updatedAt,
            string nameEn = null,
            string code = null,
            int sortOrder = 0)
        {
            SetCountryId(countryId);
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetSortOrder(sortOrder);

            SetUpdated(updatedAt);
        }

        private void SetCountryId(int countryId)
        {
            if (countryId <= 0)
                throw new DomainException("Region country id must be greater than zero.");

            CountryId = countryId;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Region name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Region name must be 100 characters or less.");

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
                throw new DomainException("Region alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Region code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 10)
                throw new DomainException("Region code must be 10 characters or less.");

            Code = code;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Region sort order cannot be negative.");

            SortOrder = sortOrder;
        }
    }
}