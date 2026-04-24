using System.Collections.Generic;
using System.Linq;
using TourCore.Domain.Common.Exceptions;

namespace TourCore.Domain.Hotels.ValueObjects
{
    /// <summary>
    /// Правило размещения для одного типа мест:
    /// основных или дополнительных.
    /// </summary>
    public class AccommodationPlacementRule
    {
        /// <summary>
        /// Количество взрослых на данном типе мест.
        /// Например: взрослых на основных местах или взрослых на дополнительных местах.
        /// </summary>
        public short AdultsCount { get; }

        /// <summary>
        /// Количество детей на данном типе мест.
        /// Например: детей на основных местах или детей на дополнительных местах.
        /// </summary>
        public short ChildrenCount { get; }

        /// <summary>
        /// Признак того, что дети на этих местах считаются инфантами.
        /// </summary>
        public bool ChildrenAreInfants { get; }

        /// <summary>
        /// Допустимые возрастные диапазоны детей для этих мест.
        /// </summary>
        public IReadOnlyCollection<AgeRange> ChildAgeRanges { get; }

        public AccommodationPlacementRule(
            short adultsCount,
            short childrenCount,
            bool childrenAreInfants,
            IEnumerable<AgeRange> childAgeRanges = null)
        {
            if (adultsCount < 0)
                throw new DomainException("Adults count cannot be negative.");

            if (childrenCount < 0)
                throw new DomainException("Children count cannot be negative.");

            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            ChildrenAreInfants = childrenAreInfants;
            ChildAgeRanges = (childAgeRanges ?? Enumerable.Empty<AgeRange>()).ToList().AsReadOnly();
        }

        /// <summary>
        /// Есть ли вообще какие-либо места в этом блоке.
        /// </summary>
        public bool HasPlaces => AdultsCount > 0 || ChildrenCount > 0;
    }
}