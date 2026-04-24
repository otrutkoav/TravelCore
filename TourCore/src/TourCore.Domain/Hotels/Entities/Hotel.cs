using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Domain.Hotels.Entities
{
    public class Hotel : AuditableEntity
    {
        public int CountryId { get; protected set; }
        public virtual Country Country { get; protected set; }

        public int CityId { get; protected set; }
        public virtual City City { get; protected set; }

        public int? ResortId { get; protected set; }
        public virtual Resort Resort { get; protected set; }

        public int? CategoryId { get; protected set; }
        public virtual HotelCategory Category { get; protected set; }

        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string Stars { get; protected set; }
        public string Code { get; protected set; }

        public string Address { get; protected set; }
        public string Phone { get; protected set; }
        public string Fax { get; protected set; }
        public string Email { get; protected set; }
        public string Website { get; protected set; }

        public string Latitude { get; protected set; }
        public string Longitude { get; protected set; }

        public bool IsCruise { get; protected set; }
        public int SortOrder { get; protected set; }
        public int? Rank { get; protected set; }

        protected Hotel()
        {
        }

        public Hotel(
            int countryId,
            int cityId,
            string name,
            string stars,
            DateTime createdAt,
            int? resortId = null,
            int? categoryId = null,
            string nameEn = null,
            string code = null,
            string address = null,
            string phone = null,
            string fax = null,
            string email = null,
            string website = null,
            string latitude = null,
            string longitude = null,
            bool isCruise = false,
            int sortOrder = 0,
            int? rank = null)
        {
            SetCountryId(countryId);
            SetCityId(cityId);
            SetResortId(resortId);
            SetCategoryId(categoryId);

            SetName(name);
            SetNameEn(nameEn);
            SetStars(stars);
            SetCode(code);

            SetAddress(address);
            SetPhone(phone);
            SetFax(fax);
            SetEmail(email);
            SetWebsite(website);

            SetLatitude(latitude);
            SetLongitude(longitude);

            SetIsCruise(isCruise);
            SetSortOrder(sortOrder);
            SetRank(rank);

            SetCreated(createdAt);
        }

        public void Update(
            int countryId,
            int cityId,
            string name,
            string stars,
            DateTime updatedAt,
            int? resortId = null,
            int? categoryId = null,
            string nameEn = null,
            string code = null,
            string address = null,
            string phone = null,
            string fax = null,
            string email = null,
            string website = null,
            string latitude = null,
            string longitude = null,
            bool isCruise = false,
            int sortOrder = 0,
            int? rank = null)
        {
            SetCountryId(countryId);
            SetCityId(cityId);
            SetResortId(resortId);
            SetCategoryId(categoryId);

            SetName(name);
            SetNameEn(nameEn);
            SetStars(stars);
            SetCode(code);

            SetAddress(address);
            SetPhone(phone);
            SetFax(fax);
            SetEmail(email);
            SetWebsite(website);

            SetLatitude(latitude);
            SetLongitude(longitude);

            SetIsCruise(isCruise);
            SetSortOrder(sortOrder);
            SetRank(rank);

            SetUpdated(updatedAt);
        }

        private void SetCountryId(int countryId)
        {
            if (countryId <= 0)
                throw new DomainException("Hotel country id must be greater than zero.");

            CountryId = countryId;
        }

        private void SetCityId(int cityId)
        {
            if (cityId <= 0)
                throw new DomainException("Hotel city id must be greater than zero.");

            CityId = cityId;
        }

        private void SetResortId(int? resortId)
        {
            if (resortId.HasValue && resortId.Value <= 0)
                throw new DomainException("Hotel resort id must be greater than zero.");

            ResortId = resortId;
        }

        private void SetCategoryId(int? categoryId)
        {
            if (categoryId.HasValue && categoryId.Value <= 0)
                throw new DomainException("Hotel category id must be greater than zero.");

            CategoryId = categoryId;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Hotel name is required.");

            name = name.Trim();

            if (name.Length > 200)
                throw new DomainException("Hotel name must be 200 characters or less.");

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

            if (nameEn.Length > 200)
                throw new DomainException("Hotel alternate name must be 200 characters or less.");

            NameEn = nameEn;
        }

        private void SetStars(string stars)
        {
            if (string.IsNullOrWhiteSpace(stars))
                throw new DomainException("Hotel stars value is required.");

            stars = stars.Trim();

            if (stars.Length > 20)
                throw new DomainException("Hotel stars value must be 20 characters or less.");

            Stars = stars;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Meal type code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 10)
                throw new DomainException("Hotel code must be 10 characters or less.");

            Code = code;
        }

        private void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                Address = null;
                return;
            }

            address = address.Trim();

            if (address.Length > 254)
                throw new DomainException("Hotel address must be 254 characters or less.");

            Address = address;
        }

        private void SetPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                Phone = null;
                return;
            }

            phone = phone.Trim();

            if (phone.Length > 50)
                throw new DomainException("Hotel phone must be 50 characters or less.");

            Phone = phone;
        }

        private void SetFax(string fax)
        {
            if (string.IsNullOrWhiteSpace(fax))
            {
                Fax = null;
                return;
            }

            fax = fax.Trim();

            if (fax.Length > 20)
                throw new DomainException("Hotel fax must be 20 characters or less.");

            Fax = fax;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Email = null;
                return;
            }

            email = email.Trim();

            if (email.Length > 50)
                throw new DomainException("Hotel email must be 50 characters or less.");

            Email = email;
        }

        private void SetWebsite(string website)
        {
            if (string.IsNullOrWhiteSpace(website))
            {
                Website = null;
                return;
            }

            website = website.Trim();

            if (website.Length > 254)
                throw new DomainException("Hotel website must be 254 characters or less.");

            Website = website;
        }

        private void SetLatitude(string latitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
            {
                Latitude = null;
                return;
            }

            latitude = latitude.Trim();

            if (latitude.Length > 30)
                throw new DomainException("Hotel latitude must be 30 characters or less.");

            Latitude = latitude;
        }

        private void SetLongitude(string longitude)
        {
            if (string.IsNullOrWhiteSpace(longitude))
            {
                Longitude = null;
                return;
            }

            longitude = longitude.Trim();

            if (longitude.Length > 30)
                throw new DomainException("Hotel longitude must be 30 characters or less.");

            Longitude = longitude;
        }

        private void SetIsCruise(bool isCruise)
        {
            IsCruise = isCruise;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Hotel sort order cannot be negative.");

            SortOrder = sortOrder;
        }

        private void SetRank(int? rank)
        {
            if (rank.HasValue && rank.Value < 0)
                throw new DomainException("Hotel rank cannot be negative.");

            Rank = rank;
        }
    }
}