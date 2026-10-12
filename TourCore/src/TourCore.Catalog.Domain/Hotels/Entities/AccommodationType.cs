using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Тип размещения.
    /// Например: SGL, DBL, TRPL, EXB и т.п.
    /// </summary>
    public class AccommodationType : AuditableEntity
    {
        public string Code { get; protected set; }

        public string Name { get; protected set; }

        public string NameEn { get; protected set; }

        public bool IsMain { get; protected set; }

        public short? AgeFrom { get; protected set; }

        public short? AgeTo { get; protected set; }

        public short? PerRoom { get; protected set; }

        public int SortOrder { get; protected set; }

        public string Description { get; protected set; }

        public int? MainPlacementRuleId { get; protected set; }

        public virtual AccommodationPlacementRule MainPlacementRule { get; protected set; }

        public int? ExtraPlacementRuleId { get; protected set; }

        public virtual AccommodationPlacementRule ExtraPlacementRule { get; protected set; }

        protected AccommodationType()
        {
        }

        public AccommodationType(
            string name,
            DateTime createdAt,
            string code = null,
            string nameEn = null,
            bool isMain = false,
            short? ageFrom = null,
            short? ageTo = null,
            short? perRoom = null,
            int sortOrder = 0,
            string description = null,
            int? mainPlacementRuleId = null,
            int? extraPlacementRuleId = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetIsMain(isMain);
            SetAgeRange(ageFrom, ageTo);
            SetPerRoom(perRoom);
            SetSortOrder(sortOrder);
            SetDescription(description);
            SetMainPlacementRuleId(mainPlacementRuleId);
            SetExtraPlacementRuleId(extraPlacementRuleId);

            SetCreated(createdAt);
        }

        public void Update(
            string name,
            DateTime updatedAt,
            string code = null,
            string nameEn = null,
            bool isMain = false,
            short? ageFrom = null,
            short? ageTo = null,
            short? perRoom = null,
            int sortOrder = 0,
            string description = null,
            int? mainPlacementRuleId = null,
            int? extraPlacementRuleId = null)
        {
            SetName(name);
            SetCode(code);
            SetNameEn(nameEn);
            SetIsMain(isMain);
            SetAgeRange(ageFrom, ageTo);
            SetPerRoom(perRoom);
            SetSortOrder(sortOrder);
            SetDescription(description);
            SetMainPlacementRuleId(mainPlacementRuleId);
            SetExtraPlacementRuleId(extraPlacementRuleId);

            SetUpdated(updatedAt);
        }

        private void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new DomainException("Accommodation type code is required.");

            code = code.Trim().ToUpperInvariant();

            if (code.Length > 50)
                throw new DomainException("Accommodation type code must be 50 characters or less.");

            Code = code;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Accommodation type name is required.");

            name = name.Trim();

            if (name.Length > 100)
                throw new DomainException("Accommodation type name must be 100 characters or less.");

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
                throw new DomainException("Accommodation type alternate name must be 100 characters or less.");

            NameEn = nameEn;
        }

        private void SetIsMain(bool isMain)
        {
            IsMain = isMain;
        }

        private void SetAgeRange(short? ageFrom, short? ageTo)
        {
            if (ageFrom.HasValue && ageFrom.Value < 0)
                throw new DomainException("Accommodation type age from cannot be negative.");

            if (ageTo.HasValue && ageTo.Value < 0)
                throw new DomainException("Accommodation type age to cannot be negative.");

            if (ageFrom.HasValue && ageTo.HasValue && ageFrom.Value > ageTo.Value)
                throw new DomainException("Accommodation type age from cannot be greater than age to.");

            AgeFrom = ageFrom;
            AgeTo = ageTo;
        }

        private void SetPerRoom(short? perRoom)
        {
            if (perRoom.HasValue && perRoom.Value < 0)
                throw new DomainException("Accommodation type per-room value cannot be negative.");

            PerRoom = perRoom;
        }

        private void SetSortOrder(int sortOrder)
        {
            if (sortOrder < 0)
                throw new DomainException("Accommodation type sort order cannot be negative.");

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

        private void SetMainPlacementRuleId(int? value)
        {
            if (value.HasValue && value.Value <= 0)
                throw new DomainException("Accommodation type main placement rule id must be greater than zero.");

            MainPlacementRuleId = value;
        }

        private void SetExtraPlacementRuleId(int? value)
        {
            if (value.HasValue && value.Value <= 0)
                throw new DomainException("Accommodation type extra placement rule id must be greater than zero.");

            ExtraPlacementRuleId = value;
        }
    }
}