namespace TourCore.Application.Finance.Rates.DTOs
{
    /// <summary>
    /// Фильтр списка валют.
    /// </summary>
    public class RateListFilter
    {
        /// <summary>
        /// Поиск по коду, названию и ISO-коду валюты.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Признак основной валюты.
        /// </summary>
        public bool? IsMain { get; set; }

        /// <summary>
        /// Признак национальной валюты.
        /// </summary>
        public bool? IsNational { get; set; }

        /// <summary>
        /// Признак отображения валюты в поиске.
        /// </summary>
        public bool? ShowInSearch { get; set; }
    }
}