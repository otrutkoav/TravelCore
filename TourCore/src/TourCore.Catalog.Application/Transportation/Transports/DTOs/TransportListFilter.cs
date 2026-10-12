namespace TourCore.Application.Transportation.Transports.DTOs
{
    /// <summary>
    /// Фильтр списка транспорта.
    /// </summary>
    public class TransportListFilter
    {
        /// <summary>
        /// Поиск по названию и английскому названию транспорта.
        /// </summary>
        public string Search { get; set; }
    }
}