namespace TourCore.Domain.Tours
{
    /// <summary>
    /// Определяет назначение справочного идентификатора в ограничении перелёта тура.
    /// Числовые значения сохраняются в базе данных и не должны изменяться.
    /// </summary>
    public enum TourFlightRestrictionType
    {
        /// <summary>Страна отправления.</summary>
        DepartureCountry = 1,

        /// <summary>Город отправления.</summary>
        DepartureCity = 2,

        /// <summary>Страна прибытия.</summary>
        ArrivalCountry = 3,

        /// <summary>Город прибытия.</summary>
        ArrivalCity = 4,

        /// <summary>Авиакомпания.</summary>
        Airline = 5,

        /// <summary>Рейс.</summary>
        Flight = 6,

        /// <summary>Аэропорт отправления.</summary>
        DepartureAirport = 7,

        /// <summary>Аэропорт прибытия.</summary>
        ArrivalAirport = 8,

        /// <summary>Тариф.</summary>
        Fare = 9,
    }
}
