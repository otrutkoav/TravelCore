using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Avia.Entities
{
    public class Airport : AuditableEntity
    {
        public int CityId { get; protected set; }
        public virtual City City { get; protected set; }

        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string IcaoCode { get; protected set; }
        public string LetterCode { get; protected set; }

        protected Airport()
        {
        }

        public Airport(
            int cityId,
            string code,
            string name,
            DateTime createdAt,
            string nameEn = null,
            string icaoCode = null,
            string letterCode = null)
        {
            SetCityId(cityId);
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);
            SetIcaoCode(icaoCode);
            SetLetterCode(letterCode);

            SetCreated(createdAt);
        }

        public void Update(
            int cityId,
            string code,
            string name,
            DateTime updatedAt,
            string nameEn = null,
            string icaoCode = null,
            string letterCode = null)
        {
            SetCityId(cityId);
            SetCode(code);
            SetName(name);
            SetNameEn(nameEn);
            SetIcaoCode(icaoCode);
            SetLetterCode(letterCode);

            SetUpdated(updatedAt);
        }

        private void SetCityId(int cityId)
        {
            if (cityId <= 0)
                throw new DomainException("Airport city id must be greater than zero.");

            CityId = cityId;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Airport code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 5)
                throw new DomainException("Airport code must be 5 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Airport name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Airport name must be 100 characters or less.");

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
                throw new DomainException("Airport alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetIcaoCode(string icaoCode)
        {
            if (string.IsNullOrWhiteSpace(icaoCode))
            {
                IcaoCode = null;
                return;
            }

            icaoCode = icaoCode.Trim().ToUpperInvariant();

            if (icaoCode.Length > 5)
                throw new DomainException("Airport ICAO code must be 5 characters or less.");

            IcaoCode = icaoCode;
        }

        private void SetLetterCode(string letterCode)
        {
            if (string.IsNullOrWhiteSpace(letterCode))
            {
                LetterCode = null;
                return;
            }

            letterCode = letterCode.Trim().ToUpperInvariant();

            if (letterCode.Length > 1)
                throw new DomainException("Airport letter code must be 1 character or less.");

            LetterCode = letterCode;
        }
    }
}