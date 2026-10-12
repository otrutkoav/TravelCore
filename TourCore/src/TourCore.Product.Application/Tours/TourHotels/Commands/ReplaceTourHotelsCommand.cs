using System.Collections.Generic;

namespace TourCore.Application.Tours
{
    /// <summary>Полностью заменяет настройки проживания услуги тура.</summary>
    public class ReplaceTourHotelsCommand
    {
        /// <summary>Идентификатор тура из маршрута запроса.</summary>
        public int TourId { get; set; }

        /// <summary>Идентификатор услуги проживания из маршрута запроса.</summary>
        public int TourServiceId { get; set; }

        /// <summary>Альтернативные варианты выполнения услуги проживания.</summary>
        public ICollection<TourHotelGroupInput> Groups { get; set; } =
            new List<TourHotelGroupInput>();
    }

    /// <summary>Входные данные альтернативного варианта проживания.</summary>
    public class TourHotelGroupInput
    {
        /// <summary>Название варианта проживания.</summary>
        public string Name { get; set; }

        /// <summary>Приоритет варианта при подборе.</summary>
        public int Priority { get; set; }

        /// <summary>Позиция группы внутри услуги, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Последовательные шаблоны проживания варианта.</summary>
        public ICollection<TourHotelInput> Hotels { get; set; } = new List<TourHotelInput>();
    }

    /// <summary>Входные данные одного шаблона проживания.</summary>
    public class TourHotelInput
    {
        /// <summary>Позиция шаблона внутри группы, начиная с 1.</summary>
        public int OrderIndex { get; set; }

        /// <summary>Идентификатор страны проживания из справочника.</summary>
        public int CountryId { get; set; }

        /// <summary>Допустимые идентификаторы городов проживания из справочника.</summary>
        public ICollection<int> CityIds { get; set; } = new List<int>();

        /// <summary>Допустимые продолжительности проживания в ночах.</summary>
        public ICollection<int> DurationsInNights { get; set; } = new List<int>();

        /// <summary>Идентификаторы разрешённых отелей выбранного города.</summary>
        public ICollection<int> HotelIds { get; set; } = new List<int>();
    }
}
