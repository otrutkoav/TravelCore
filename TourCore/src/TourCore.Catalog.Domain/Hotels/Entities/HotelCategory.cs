using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    public class HotelCategory : AuditableEntity
    {
        public string Name { get; protected set; }
        public string NameEn { get; protected set; }
        public int? PrintOrder { get; protected set; }
        public string GlobalCode { get; protected set; }
        public string Description { get; protected set; }

        protected HotelCategory()
        {
        }

        public HotelCategory(
            string name,
            DateTime createdAt,
            string nameEn = null,
            int? printOrder = null,
            string globalCode = null,
            string description = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetPrintOrder(printOrder);
            SetGlobalCode(globalCode);
            SetDescription(description);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string nameEn = null,
            int? printOrder = null,
            string globalCode = null,
            string description = null)
        {
            SetName(name);
            SetNameEn(nameEn);
            SetPrintOrder(printOrder);
            SetGlobalCode(globalCode);
            SetDescription(description);

            SetUpdated(updatedAt);
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Hotel category name is required.");

            name = name.Trim();

            if (name.Length > 50)
                throw new DomainException("Hotel category name must be 50 characters or less.");

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

            if (nameEn.Length > 50)
                throw new DomainException("Hotel category alternate name must be 50 characters or less.");

            NameEn = nameEn;
        }

        private void SetPrintOrder(int? printOrder)
        {
            if (printOrder.HasValue && printOrder.Value < 0)
                throw new DomainException("Hotel category print order cannot be negative.");

            PrintOrder = printOrder;
        }

        private void SetGlobalCode(string globalCode)
        {
            if (string.IsNullOrWhiteSpace(globalCode))
            {
                GlobalCode = null;
                return;
            }

            globalCode = globalCode.Trim().ToUpperInvariant();

            if (globalCode.Length > 20)
                throw new DomainException("Hotel category global code must be 20 characters or less.");

            GlobalCode = globalCode;
        }

        private void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Description = null;
                return;
            }

            description = description.Trim();

            Description = description;
        }
    }
}