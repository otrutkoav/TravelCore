using System;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Возрастной диапазон детей для правила размещения.
    /// </summary>
    public class AccommodationPlacementRuleAgeRange : AuditableEntity
    {
        public int AccommodationPlacementRuleId { get; protected set; }

        public virtual AccommodationPlacementRule AccommodationPlacementRule { get; protected set; }

        public short AgeFrom { get; protected set; }

        public short AgeTo { get; protected set; }

        protected AccommodationPlacementRuleAgeRange()
        {
        }

        public AccommodationPlacementRuleAgeRange(
            int accommodationPlacementRuleId,
            short ageFrom,
            short ageTo,
            DateTime createdAt)
        {
            SetAccommodationPlacementRuleId(accommodationPlacementRuleId);
            SetAgeRange(ageFrom, ageTo);

            SetCreated(createdAt);
        }

        public void Update(
            int accommodationPlacementRuleId,
            short ageFrom,
            short ageTo,
            DateTime updatedAt)
        {
            SetAccommodationPlacementRuleId(accommodationPlacementRuleId);
            SetAgeRange(ageFrom, ageTo);

            SetUpdated(updatedAt);
        }

        private void SetAccommodationPlacementRuleId(int value)
        {
            if (value <= 0)
                throw new DomainException("Accommodation placement rule id must be greater than zero.");

            AccommodationPlacementRuleId = value;
        }

        private void SetAgeRange(short ageFrom, short ageTo)
        {
            if (ageFrom < 0)
                throw new DomainException("Age from cannot be negative.");

            if (ageTo < 0)
                throw new DomainException("Age to cannot be negative.");

            if (ageFrom > ageTo)
                throw new DomainException("Age from cannot be greater than age to.");

            AgeFrom = ageFrom;
            AgeTo = ageTo;
        }
    }
}