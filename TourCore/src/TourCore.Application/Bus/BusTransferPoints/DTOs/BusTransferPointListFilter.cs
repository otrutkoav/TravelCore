namespace TourCore.Application.Bus.BusTransferPoints.DTOs
{
    /// <summary>
    /// Фильтр списка точек автобусных переездов.
    /// </summary>
    public class BusTransferPointListFilter
    {
        /// <summary>
        /// Идентификатор автобусного переезда.
        /// </summary>
        public int? BusTransferId { get; set; }

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