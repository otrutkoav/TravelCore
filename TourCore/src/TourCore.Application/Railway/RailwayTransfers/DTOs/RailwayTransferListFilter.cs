namespace TourCore.Application.Railway.RailwayTransfers.DTOs
{
    /// <summary>
    /// Фильтр списка железнодорожных переездов.
    /// </summary>
    public class RailwayTransferListFilter
    {
        /// <summary>
        /// Поиск по названию железнодорожного переезда.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Идентификатор страны отправления.
        /// </summary>
        public int? CountryFromId { get; set; }

        /// <summary>
        /// Идентификатор города отправления.
        /// </summary>
        public int? CityFromId { get; set; }

        /// <summary>
        /// Идентификатор страны прибытия.
        /// </summary>
        public int? CountryToId { get; set; }

        /// <summary>
        /// Идентификатор города прибытия.
        /// </summary>
        public int? CityToId { get; set; }
    }
}