namespace TourCore.Application.Bus.BusTransfers.DTOs
{
    /// <summary>
    /// Фильтр списка автобусных переездов.
    /// </summary>
    public class BusTransferListFilter
    {
        /// <summary>
        /// Поиск по названию автобусного переезда.
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