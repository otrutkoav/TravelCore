using System;

namespace TourCore.Search.Contracts
{
    /// <summary>Конкретный транспортный сегмент на дату.</summary>
    public sealed class RouteSegment
    {
        /// <summary>Идентификатор базового маршрута.</summary>
        public int RouteId { get; set; }
        /// <summary>Идентификатор расписания или сезона.</summary>
        public int ScheduleId { get; set; }
        /// <summary>Название маршрута или номер рейса.</summary>
        public string Name { get; set; }
        /// <summary>Идентификатор города отправления.</summary>
        public int DepartureCityId { get; set; }
        /// <summary>Название города отправления.</summary>
        public string DepartureCityName { get; set; }
        /// <summary>Идентификатор города прибытия.</summary>
        public int ArrivalCityId { get; set; }
        /// <summary>Название города прибытия.</summary>
        public string ArrivalCityName { get; set; }
        /// <summary>Дата и время отправления.</summary>
        public DateTime DepartureDateTime { get; set; }
        /// <summary>Дата и время прибытия с учётом дней в пути.</summary>
        public DateTime ArrivalDateTime { get; set; }
        /// <summary>Код перевозчика, если он задан источником.</summary>
        public string CarrierCode { get; set; }
        /// <summary>Номер рейса или маршрута, если он задан источником.</summary>
        public string Number { get; set; }
    }
}
