using System;
using System.Collections.Generic;

namespace TourCore.Contracts.Tours
{
    /// <summary>Настройки железнодорожного переезда в составе услуги тура.</summary>
    public class TourRailwayTransferDto
    {
        /// <summary>Идентификатор настроек железнодорожного переезда.</summary>
        public int Id { get; set; }

        /// <summary>Допустимые идентификаторы стран отправления.</summary>
        public IReadOnlyCollection<int> DepartureCountryIds { get; set; }

        /// <summary>Допустимые идентификаторы городов отправления.</summary>
        public IReadOnlyCollection<int> DepartureCityIds { get; set; }

        /// <summary>Допустимые идентификаторы стран прибытия.</summary>
        public IReadOnlyCollection<int> ArrivalCountryIds { get; set; }

        /// <summary>Допустимые идентификаторы городов прибытия.</summary>
        public IReadOnlyCollection<int> ArrivalCityIds { get; set; }

        /// <summary>Допустимые идентификаторы записей справочника RailwayTransfers.</summary>
        public IReadOnlyCollection<int> RailwayTransferIds { get; set; }

        /// <summary>Допустимые идентификаторы транспорта.</summary>
        public IReadOnlyCollection<int> TransportIds { get; set; }

        /// <summary>Начало допустимого интервала времени отправления.</summary>
        public TimeSpan DepartureTimeFrom { get; set; }

        /// <summary>Окончание допустимого интервала времени отправления.</summary>
        public TimeSpan DepartureTimeTo { get; set; }

        /// <summary>Начало допустимого интервала времени прибытия.</summary>
        public TimeSpan ArrivalTimeFrom { get; set; }

        /// <summary>Окончание допустимого интервала времени прибытия.</summary>
        public TimeSpan ArrivalTimeTo { get; set; }
    }
}
