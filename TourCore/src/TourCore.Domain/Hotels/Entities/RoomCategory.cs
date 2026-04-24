using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Категория номера.
    /// Например: Standard, Superior, Deluxe, Family и т.д.
    /// </summary>
    public class RoomCategory : AuditableEntity
    {
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public int SortOrder { get; protected set; }
        public string Description { get; protected set; }

        protected RoomCategory()
        {
        }

        public RoomCategory(
            string name,
            DateTime createdAt,
            string code = null,
            string nameEn = null,
            int sortOrder = 0,
            string description = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetSortOrder(sortOrder);
            SetDescription(description);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string code = null,
            string nameEn = null,
            int sortOrder = 0,
            string description = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetSortOrder(sortOrder);
            SetDescription(description);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Meal type code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 40)
                throw new DomainException("Room category code must be 40 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Room category name is required.");

            name = name.Trim();

            if (name.Length > 150)
                throw new DomainException("Room category name must be 150 characters or less.");

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
                throw new DomainException("Room category alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Room category sort order cannot be negative.");

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