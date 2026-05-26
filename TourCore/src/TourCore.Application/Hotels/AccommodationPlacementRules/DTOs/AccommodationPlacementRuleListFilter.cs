namespace TourCore.Application.Hotels.AccommodationPlacementRules.DTOs
{
    public class AccommodationPlacementRuleListFilter
    {
        /// <summary>
        /// Фильтр по количеству взрослых.
        /// </summary>
        public short? AdultsCount { get; set; }

        /// <summary>
        /// Фильтр по количеству детей.
        /// </summary>
        public short? ChildrenCount { get; set; }

        /// <summary>
        /// Фильтр по признаку, что дети считаются инфантами.
        /// </summary>
        public bool? ChildrenAreInfants { get; set; }
    }
}