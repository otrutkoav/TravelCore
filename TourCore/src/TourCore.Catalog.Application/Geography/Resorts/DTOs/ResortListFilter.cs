namespace TourCore.Application.Geography.Resorts.DTOs
{
    /// <summary>
    /// Фильтр списка курортов.
    /// </summary>
    public class ResortListFilter
    {
        /// <summary>
        /// Поиск по названию и английскому названию курорта.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Идентификатор страны.
        /// </summary>
        public int? CountryId { get; set; }
    }
}