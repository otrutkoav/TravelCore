using System;
using System.Collections.Generic;

namespace TourCore.Application.Tours
{
    /// <summary>Создаёт или полностью заменяет настройки автобусного переезда.</summary>
    public class ReplaceTourBusTransferCommand
    {
        /// <summary>Идентификатор тура из маршрута запроса.</summary>
        public int TourId { get; set; }

        /// <summary>Идентификатор услуги автобусного переезда из маршрута запроса.</summary>
        public int TourServiceId { get; set; }

        /// <summary>Допустимые идентификаторы стран отправления.</summary>
        public ICollection<int> DepartureCountryIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы городов отправления.</summary>
        public ICollection<int> DepartureCityIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы стран прибытия.</summary>
        public ICollection<int> ArrivalCountryIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы городов прибытия.</summary>
        public ICollection<int> ArrivalCityIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы записей справочника BusTransfers.</summary>
        public ICollection<int> BusTransferIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы мест посадки; пустой набор не ограничивает место.</summary>
        public ICollection<int> BoardingPointIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы транспорта; пустой набор не ограничивает транспорт.</summary>
        public ICollection<int> TransportIds { get; set; } = new List<int>();

        /// <summary>Начало допустимого интервала времени отправления.</summary>
        public TimeSpan DepartureTimeFrom { get; set; } = TimeSpan.Zero;

        /// <summary>Окончание допустимого интервала времени отправления.</summary>
        public TimeSpan DepartureTimeTo { get; set; } = new TimeSpan(23, 59, 59);

        /// <summary>Начало допустимого интервала времени прибытия.</summary>
        public TimeSpan ArrivalTimeFrom { get; set; } = TimeSpan.Zero;

        /// <summary>Окончание допустимого интервала времени прибытия.</summary>
        public TimeSpan ArrivalTimeTo { get; set; } = new TimeSpan(23, 59, 59);
    }
}
