using System;
using System.Collections.Generic;
using TourCore.Shared.Primitives;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.Entities
{
    /// <summary>
    /// Правило размещения.
    /// Описывает количество взрослых, детей и возрастные диапазоны детей.
    /// Может переиспользоваться разными типами размещения.
    /// </summary>
    public class AccommodationPlacementRule : AuditableEntity
    {
        public short AdultsCount { get; protected set; }

        public short ChildrenCount { get; protected set; }

        public bool ChildrenAreInfants { get; protected set; }

        public virtual ICollection<AccommodationPlacementRuleAgeRange> ChildAgeRanges { get; protected set; }

        protected AccommodationPlacementRule()
        {
            ChildAgeRanges = new List<AccommodationPlacementRuleAgeRange>();
        }

        public AccommodationPlacementRule(
            short adultsCount,
            short childrenCount,
            bool childrenAreInfants,
            DateTime createdAt)
        {
            ChildAgeRanges = new List<AccommodationPlacementRuleAgeRange>();

            SetAdultsCount(adultsCount);
            SetChildrenCount(childrenCount);
            SetChildrenAreInfants(childrenAreInfants);

            SetCreated(createdAt);
        }

        public void Update(
            short adultsCount,
            short childrenCount,
            bool childrenAreInfants,
            DateTime updatedAt)
        {
            SetAdultsCount(adultsCount);
            SetChildrenCount(childrenCount);
            SetChildrenAreInfants(childrenAreInfants);

            SetUpdated(updatedAt);
        }

        private void SetAdultsCount(short value)
        {
            if (value < 0)
                throw new DomainException("Accommodation placement rule adults count cannot be negative.");

            AdultsCount = value;
        }

        private void SetChildrenCount(short value)
        {
            if (value < 0)
                throw new DomainException("Accommodation placement rule children count cannot be negative.");

            ChildrenCount = value;
        }

        private void SetChildrenAreInfants(bool value)
        {
            ChildrenAreInfants = value;
        }
    }
}