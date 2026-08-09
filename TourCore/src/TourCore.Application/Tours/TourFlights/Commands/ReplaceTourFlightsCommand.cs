using System.Collections.Generic;

namespace TourCore.Application.Tours
{
    /// <summary>Полностью заменяет авиационные настройки услуги тура.</summary>
    public class ReplaceTourFlightsCommand
    {
        /// <summary>Идентификатор тура из маршрута запроса.</summary>
        public int TourId { get; set; }

        /// <summary>Идентификатор услуги «Авиаперелёт» из маршрута запроса.</summary>
        public int TourServiceId { get; set; }

        /// <summary>Альтернативные варианты выполнения авиационной услуги.</summary>
        public ICollection<TourFlightGroupInput> Groups { get; set; } =
            new List<TourFlightGroupInput>();
    }

    public class TourFlightGroupInput
    {
        /// <summary>Название варианта перелёта.</summary>
        public string Name { get; set; }

        /// <summary>Приоритет варианта.</summary>
        public int Priority { get; set; }

        /// <summary>Порядок отображения варианта.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Последовательные перелёты варианта.</summary>
        public ICollection<TourFlightInput> Flights { get; set; } = new List<TourFlightInput>();
    }

    public class TourFlightInput
    {
        /// <summary>Позиция перелёта внутри группы, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Допустимые идентификаторы стран вылета.</summary>
        public ICollection<int> DepartureCountryIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы городов вылета.</summary>
        public ICollection<int> DepartureCityIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы стран прилёта.</summary>
        public ICollection<int> ArrivalCountryIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы городов прилёта.</summary>
        public ICollection<int> ArrivalCityIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы авиакомпаний.</summary>
        public ICollection<int> AirlineIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы рейсов.</summary>
        public ICollection<int> FlightIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы аэропортов вылета.</summary>
        public ICollection<int> DepartureAirportIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы аэропортов прилёта.</summary>
        public ICollection<int> ArrivalAirportIds { get; set; } = new List<int>();

        /// <summary>Допустимые идентификаторы тарифов.</summary>
        public ICollection<int> FareIds { get; set; } = new List<int>();

        /// <summary>Минимальный интервал после предыдущего перелёта, в минутах.</summary>
        public int? MinConnectionMinutes { get; set; }

        /// <summary>Максимальный интервал после предыдущего перелёта, в минутах.</summary>
        public int? MaxConnectionMinutes { get; set; }
    }
}
