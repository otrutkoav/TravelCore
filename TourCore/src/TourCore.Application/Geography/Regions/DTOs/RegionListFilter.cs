namespace TourCore.Application.Geography.Regions.DTOs
{
    /// <summary>
    /// Фильтр списка регионов.
    /// </summary>
    public class RegionListFilter
    {
        /// <summary>
        /// Поиск по названию, английскому названию и коду региона.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Идентификатор страны.
        /// </summary>
        public int? CountryId { get; set; }
    }
}