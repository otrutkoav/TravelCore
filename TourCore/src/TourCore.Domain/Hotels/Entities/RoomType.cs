using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Тип номера.
    /// Справочник видов номеров: Standard, Deluxe, Suite и т.д.
    /// </summary>
    public class RoomType : AuditableEntity
    {
        /// <summary>
        /// Код типа номера.
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Основное название типа номера.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Альтернативное / латинское название типа номера.
        /// </summary>
        public string NameEn { get; protected set; }

        /// <summary>
        /// Количество основных мест.
        /// </summary>
        public short? Places { get; protected set; }

        /// <summary>
        /// Количество дополнительных мест.
        /// </summary>
        public short? ExtraPlaces { get; protected set; }

        /// <summary>
        /// Порядок сортировки.
        /// </summary>
        public int SortOrder { get; protected set; }

        /// <summary>
        /// Описание типа номера.
        /// </summary>
        public string Description { get; protected set; }

        protected RoomType()
        {
        }

        public RoomType(
            string name,
            DateTime createdAt,
            string code = null,
            string nameEn = null,
            short? places = null,
            short? extraPlaces = null,
            int sortOrder = 0,
            string description = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetPlaces(places);
            SetExtraPlaces(extraPlaces);
            SetSortOrder(sortOrder);
            SetDescription(description);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string code = null,
            string nameEn = null,
            short? places = null,
            short? extraPlaces = null,
            int sortOrder = 0,
            string description = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetPlaces(places);
            SetExtraPlaces(extraPlaces);
            SetSortOrder(sortOrder);
            SetDescription(description);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Meal type code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 25)
                throw new DomainException("Room type code must be 25 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Room type name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Room type name must be 100 characters or less.");

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
                throw new DomainException("Room type alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetPlaces(short? places)
        {
            if (places.HasValue && places.Value < 0)
                throw new DomainException("Room type places cannot be negative.");

            Places = places;
        }

        private void SetExtraPlaces(short? extraPlaces)
        {
            if (extraPlaces.HasValue && extraPlaces.Value < 0)
                throw new DomainException("Room type extra places cannot be negative.");

            ExtraPlaces = extraPlaces;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Room type sort order cannot be negative.");

            SortOrder = sortOrder;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Description = null;
                return;
            }

            Description = description.Trim();
        }
    }
}