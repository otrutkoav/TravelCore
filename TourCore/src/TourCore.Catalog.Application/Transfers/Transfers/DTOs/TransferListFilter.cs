namespace TourCore.Application.Transfers.Transfers.DTOs
{
    /// <summary>
    /// Фильтр списка трансферов.
    /// </summary>
    public class TransferListFilter
    {
        /// <summary>
        /// Поиск по названию трансфера.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Идентификатор города.
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Идентификатор направления трансфера.
        /// </summary>
        public int? DirectionId { get; set; }

        /// <summary>
        /// Признак основного трансфера.
        /// </summary>
        public bool? IsMain { get; set; }
    }
}