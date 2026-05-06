namespace TourCore.Application.Geography.Countries.DTOs
{
    /// <summary>
    /// Фильтр списка стран.
    /// </summary>
    public class CountryListFilter
    {
        /// <summary>
        /// Строка поиска по названию, английскому названию, коду, ISO2, ISO3, цифровому коду и названию гражданства.
        /// </summary>
        public string Search { get; set; }
    }
}