using System.Collections.Generic;

namespace TourCore.Contracts.Tours
{
    /// <summary>Один последовательный перелёт внутри варианта авиационной услуги тура.</summary>
    public class TourFlightDto
    {
        /// <summary>Идентификатор перелёта тура.</summary>
        public int Id { get; set; }

        /// <summary>Позиция перелёта внутри группы, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Допустимые идентификаторы стран вылета.</summary>
        public IReadOnlyCollection<int> DepartureCountryIds { get; set; }

        /// <summary>Допустимые идентификаторы городов вылета.</summary>
        public IReadOnlyCollection<int> DepartureCityIds { get; set; }

        /// <summary>Допустимые идентификаторы стран прилёта.</summary>
        public IReadOnlyCollection<int> ArrivalCountryIds { get; set; }

        /// <summary>Допустимые идентификаторы городов прилёта.</summary>
        public IReadOnlyCollection<int> ArrivalCityIds { get; set; }

        /// <summary>Допустимые идентификаторы авиакомпаний.</summary>
        public IReadOnlyCollection<int> AirlineIds { get; set; }

        /// <summary>Допустимые идентификаторы рейсов.</summary>
        public IReadOnlyCollection<int> FlightIds { get; set; }

        /// <summary>Допустимые идентификаторы аэропортов вылета.</summary>
        public IReadOnlyCollection<int> DepartureAirportIds { get; set; }

        /// <summary>Допустимые идентификаторы аэропортов прилёта.</summary>
        public IReadOnlyCollection<int> ArrivalAirportIds { get; set; }

        /// <summary>Допустимые идентификаторы тарифов.</summary>
        public IReadOnlyCollection<int> FareIds { get; set; }

        /// <summary>Минимальный интервал после предыдущего перелёта в минутах.</summary>
        public int? MinConnectionMinutes { get; set; }

        /// <summary>Максимальный интервал после предыдущего перелёта в минутах.</summary>
        public int? MaxConnectionMinutes { get; set; }
    }
}
