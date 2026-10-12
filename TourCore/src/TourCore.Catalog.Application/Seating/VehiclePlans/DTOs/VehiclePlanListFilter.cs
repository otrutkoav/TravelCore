namespace TourCore.Application.Seating.VehiclePlans.DTOs
{
    public class VehiclePlanListFilter
    {
        /// <summary>
        /// Фильтр по идентификатору транспорта.
        /// </summary>
        public int? TransportId { get; set; }

        /// <summary>
        /// Фильтр по признаку схемы самолета.
        /// </summary>
        public bool? IsAircraft { get; set; }

        /// <summary>
        /// Поиск по названию схемы транспорта.
        /// </summary>
        public string Search { get; set; }
    }
}