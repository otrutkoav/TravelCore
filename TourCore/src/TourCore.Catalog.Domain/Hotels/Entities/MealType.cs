using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    public class MealType : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public string Code { get; protected set; }
        public string GlobalCode { get; protected set; }
        public int SortOrder { get; protected set; }
        public string Description { get; protected set; }

        protected MealType()
        {
        }

        public MealType(
            string name,
            DateTime createdAt,
            string nameEn = null,
            string code = null,
            string globalCode = null,
            int sortOrder = 0,
            string description = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetGlobalCode(globalCode);
            SetSortOrder(sortOrder);
            SetDescription(description);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string nameEn = null,
            string code = null,
            string globalCode = null,
            int sortOrder = 0,
            string description = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetCode(code);
            SetGlobalCode(globalCode);
            SetSortOrder(sortOrder);
            SetDescription(description);

            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Meal type name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Meal type name must be 100 characters or less.");

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
                throw new DomainException("Meal type alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Meal type code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 30)
                throw new DomainException("Meal type code must be 30 characters or less.");

            Code = code;
        }

        private void SetGlobalCode(string globalCode)
        {
            if (string.IsNullOrWhiteSpace(globalCode))
            {
                GlobalCode = null;
                return;
            }

            globalCode = globalCode.Trim().ToUpperInvariant();

            if (globalCode.Length > 10)
                throw new DomainException("Meal type global code must be 10 characters or less.");

            GlobalCode = globalCode;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Meal type sort order cannot be negative.");

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