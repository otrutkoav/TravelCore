using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Geography.Entities
{
    public class Country : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string Code { get; protected set; }
        public string IsoCode2 { get; protected set; }
        public string IsoCode3 { get; protected set; }
        public string DigitalCode { get; protected set; }
        public string CitizenshipName { get; protected set; }
        public string CitizenshipNameEn { get; protected set; }
        public int SortOrder { get; protected set; }

        protected Country()
        {
        }

        public Country(
            string name,
            string nameEn,
            string code,
            string isoCode2,
            string isoCode3,
            int sortOrder,
            DateTime createdAt,
            string digitalCode = null,
            string citizenshipName = null,
            string citizenshipNameEn = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetIsoCode2(isoCode2);
            SetIsoCode3(isoCode3);
            SetSortOrder(sortOrder);
            SetDigitalCode(digitalCode);
            SetCitizenshipName(citizenshipName);
            SetCitizenshipNameEn(citizenshipNameEn);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            string nameEn,
            string code,
            string isoCode2,
            string isoCode3,
            int sortOrder,
            DateTime updatedAt,
            string digitalCode = null,
            string citizenshipName = null,
            string citizenshipNameEn = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetIsoCode2(isoCode2);
            SetIsoCode3(isoCode3);
            SetSortOrder(sortOrder);
            SetDigitalCode(digitalCode);
            SetCitizenshipName(citizenshipName);
            SetCitizenshipNameEn(citizenshipNameEn);

            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Country name is required.");

            name = name.Trim();

            if (name.Length > 25)
                throw new DomainException("Country name must be 25 characters or less.");

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

            if (nameEn.Length > 25)
                throw new DomainException("Country alternate name must be 25 characters or less.");

            NameEn = nameEn;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Country code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 3)
                throw new DomainException("Country code must be 3 characters or less.");

            Code = code;
        }

        private void SetIsoCode2(string isoCode2)
        {
            if (string.IsNullOrWhiteSpace(isoCode2))
                throw new DomainException("Country ISO2 code is required.");

            isoCode2 = isoCode2.Trim().ToUpperInvariant();

            if (isoCode2.Length != 2)
                throw new DomainException("Country ISO2 code must contain exactly 2 characters.");

            IsoCode2 = isoCode2;
        }

        private void SetIsoCode3(string isoCode3)
        {
            if (string.IsNullOrWhiteSpace(isoCode3))
                throw new DomainException("Country ISO3 code is required.");

            isoCode3 = isoCode3.Trim().ToUpperInvariant();

            if (isoCode3.Length != 3)
                throw new DomainException("Country ISO3 code must contain exactly 3 characters.");

            IsoCode3 = isoCode3;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Country sort order cannot be negative.");

            SortOrder = sortOrder;
        }

        private void SetDigitalCode(string digitalCode)
        {
            if (string.IsNullOrWhiteSpace(digitalCode))
            {
                DigitalCode = null;
                return;
            }

            digitalCode = digitalCode.Trim();

            if (digitalCode.Length > 3)
                throw new DomainException("Country digital code must be 3 characters or less.");

            DigitalCode = digitalCode;
        }

        private void SetCitizenshipName(string citizenshipName)
        {
            if (string.IsNullOrWhiteSpace(citizenshipName))
            {
                CitizenshipName = null;
                return;
            }

            citizenshipName = citizenshipName.Trim();

            if (citizenshipName.Length > 50)
                throw new DomainException("Country citizenship name must be 50 characters or less.");

            CitizenshipName = citizenshipName;
        }

        private void SetCitizenshipNameEn(string citizenshipNameEn)
        {
            if (string.IsNullOrWhiteSpace(citizenshipNameEn))
            {
                CitizenshipNameEn = null;
                return;
            }

            citizenshipNameEn = citizenshipNameEn.Trim();

            if (citizenshipNameEn.Length > 50)
                throw new DomainException("Country alternate citizenship name must be 50 characters or less.");

            CitizenshipNameEn = citizenshipNameEn;
        }
    }
}